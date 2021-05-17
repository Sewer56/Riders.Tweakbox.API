using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.Application.Models;
using Riders.Tweakbox.API.Application.Models.Config;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Common;
using Riders.Tweakbox.API.Domain.Models;
using Riders.Tweakbox.API.Domain.Models.Database;
using Riders.Tweakbox.API.Infrastructure.Common;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ApplicationDbContext _context;
        private IDateTimeService _dateTimeService;

        public IdentityService(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters, ApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _dateTimeService = dateTimeService;
        }

        /// <inheritdoc />
        public async Task<AuthenticationResult> TryRegisterDefaultAdminUserAsync(string email, string username, string password, CancellationToken cancellationToken)
        {
            if (!_userManager.Users.AsNoTracking().Any())
            {
                var result = await RegisterAsync(email, username, password, Country.GBR, cancellationToken);
                await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(username), Roles.Admin);
                return result;
            }
            
            return new AuthenticationResult() { Success = true };
        }

        /// <inheritdoc />
        public async Task<List<UserDetailsResult>> GetAll(PaginationQuery paginationQuery, CancellationToken token)
        {
            int skip  = paginationQuery.PageSize * paginationQuery.PageNumber;
            var users = await _context.Users.AsNoTracking().Skip(skip)
                .Take(paginationQuery.PageSize)
                .ToListAsync(token);

            var result  = new List<UserDetailsResult>(users.Count);
            foreach (var user in users)
                result.Add(Mapping.Mapper.Map<UserDetailsResult>(user));

            return result;
        }

        /// <inheritdoc />
        public async Task<UserDetailsResult> Get(int id, CancellationToken token)
        {
            var user = await _context.Users.SingleOrDefaultAsync(applicationUser => applicationUser.Id == id, token);
            return user == null ? null : Mapping.Mapper.Map<UserDetailsResult>(user);
        }

        /// <inheritdoc />
        public async Task<AuthenticationResult> RegisterAsync(string email, string username, string password, Country country, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                return new AuthenticationResult()
                {
                    Success = false, 
                    Errors = new []{ "User with this username already exists." }
                };
            }

            var newUser = new ApplicationUser()
            {
                Email = email,
                UserName = username,
                Country = country
            };

            var createUser = await _userManager.CreateAsync(newUser, password);

            if (!createUser.Succeeded)
                return new AuthenticationResult() { Errors = createUser.Errors.Select(x => x.Description) };

            await _userManager.AddToRoleAsync(newUser, Roles.User);
            var result = await GenerateAuthenticationResultForUser(newUser, false);
            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

        /// <inheritdoc />
        public async Task<AuthenticationResult> LoginAsync(string username, string password, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser == null)
            {
                return new AuthenticationResult()
                {
                    Success = false, 
                    Errors = new []{ "User with this username does not exist." }
                };
            }

            var hasValidPassword = await _userManager.CheckPasswordAsync(existingUser, password);
            if (!hasValidPassword)
            {
                return new AuthenticationResult()
                {
                    Success = false, 
                    Errors = new []{ "User/password combination is incorrect." }
                };
            }

            return await GenerateAuthenticationResultForUser(existingUser);
        }

        /// <inheritdoc />
        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
        {
            // Validate Token
            var validatedToken = GetPrincipalFromToken(token);
            if (validatedToken == null)
                return new AuthenticationResult() { Errors = new []{ "Invalid Token" }};

            // Check if token exists
            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);
            if (storedRefreshToken == null)
                return new AuthenticationResult() { Errors = new []{ "This refresh token doesn't exist." }};

            // Check Token Expiry
            if (_dateTimeService.GetCurrentDateTime() > storedRefreshToken.ExpiryDate)
                return new AuthenticationResult() { Errors = new []{ "This token has expired." }};

            if (storedRefreshToken.Invalidated)
                return new AuthenticationResult() { Errors = new []{ "This token has been invalidated." }};

            if (storedRefreshToken.Used)
                return new AuthenticationResult() { Errors = new []{ "This token has been used." }};

            if (storedRefreshToken.JwtId != jti)
                return new AuthenticationResult() { Errors = new []{ "The refresh token does not match this JWT." }};

            // Save Token
            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            var user   = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = await GenerateAuthenticationResultForUser(user, false);
            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUser(ApplicationUser user, bool saveChanges = true)
        {
            // Create Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            // Populate Claims
            var claims = new List<Claim>(new []
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            });

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Create Refresh Token
            var refreshToken = new RefreshToken()
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                ApplicationUserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(Constants.Auth.RefreshTokenExpiryDays)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            if (saveChanges)
                await _context.SaveChangesAsync();

            // Return
            return new AuthenticationResult()
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                return !IsJwtWithValidAlgorithm(validatedToken) ? null : principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool IsJwtWithValidAlgorithm(SecurityToken token)
        {
            return (token is JwtSecurityToken jwtSecurityToken) && 
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase);
        }
    }
}
