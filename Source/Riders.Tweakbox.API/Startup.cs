using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Validation;
using Riders.Tweakbox.API.Application.Models.Config;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Common;
using Riders.Tweakbox.API.Domain.Models.Database;
using Riders.Tweakbox.API.Infrastructure.Common;
using Riders.Tweakbox.API.Infrastructure.Services;

namespace Riders.Tweakbox.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("Tweakbox"))); // Needed for dotnet ef.

            // Services
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddSingleton<IServerBrowserService, ServerBrowserService>();
            services.AddSingleton(Configuration);

            // Controller
            services.AddControllers();

            // Auth
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = false
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorizationCore();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit           = true;
                options.Password.RequireLowercase       = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase       = false;
                options.Password.RequiredLength         = Constants.Auth.MinPasswordLength;
                options.Password.RequiredUniqueChars    = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(Constants.Auth.LockoutTimeSpan);
                options.Lockout.MaxFailedAccessAttempts = Constants.Auth.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers      = false;

                // User settings.
                options.User.RequireUniqueEmail        = false;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    return new BadRequestObjectResult(
                        new ErrorReponse()
                        {
                            Errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                        });
                };
            });

            // Middleware
            services.AddMvcCore().AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<GetMatchResultValidator>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Riders.Tweakbox.API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                c.IncludeXmlComments(GetXmlPathForAssembly(Assembly.GetExecutingAssembly()));        // API
                c.IncludeXmlComments(GetXmlPathForAssembly(typeof(Player).Assembly));                // Domain
                c.IncludeXmlComments(GetXmlPathForAssembly(typeof(ApplicationDbContext).Assembly));  // Infrastructure
                c.IncludeXmlComments(GetXmlPathForAssembly(typeof(IIdentityService).Assembly));      // Application

                // Authentication
                var scheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                };

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, scheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() { { scheme, Array.Empty<string>() } });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 3rd Party Middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Riders.Tweakbox.API v1");
            });

            // 1st Party Middleware
            app.UseForwardedHeaders();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private string GetXmlPathForAssembly(Assembly asm) { return Path.Combine(AppContext.BaseDirectory, $"{asm.GetName().Name}.xml"); }
    }
}
