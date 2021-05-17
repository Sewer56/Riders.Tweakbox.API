using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
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
using Riders.Tweakbox.API.Application.Commands.v1;
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
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Database
            var databasePath = Configuration.GetConnectionString("Tweakbox");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(databasePath)); // Needed for dotnet ef.

            // Services
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IStatisticsCalculatorService, StatisticsCalculatorService>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddSingleton<IServerBrowserService, ServerBrowserService>();
            services.AddSingleton<IGeoIpService, GeoIpService>();
            services.AddSingleton(Configuration);

            var geoIpSettings = new GeoIpSettings();
            Configuration.Bind(nameof(GeoIpSettings), geoIpSettings);
            services.AddSingleton(geoIpSettings);

            // Controller
            services.AddControllers();

            // Auth
            AddJwtAuthentication(services);
            services.AddAuthorizationCore();
            AddIdentity(services);
            AddErrorHandling(services);

            // Middleware
            services.AddMvcCore().AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<GetMatchResultValidator>();
            });

            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            // Environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 1st Party Middleware
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 3rd Party Middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Riders.Tweakbox.API v1");
            });
        }

        private string GetXmlPathForAssembly(Assembly asm) { return Path.Combine(AppContext.BaseDirectory, $"{asm.GetName().Name}.xml"); }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Riders.Tweakbox.API", Version = "v1"});

                // Set the comments path for the Swagger JSON and UI.
                c.IncludeXmlComments(GetXmlPathForAssembly(Assembly.GetExecutingAssembly())); // API
                c.IncludeXmlComments(GetXmlPathForAssembly(typeof(ApplicationUser).Assembly)); // Domain
                c.IncludeXmlComments(GetXmlPathForAssembly(typeof(ApplicationDbContext).Assembly)); // Infrastructure
                c.IncludeXmlComments(GetXmlPathForAssembly(typeof(IIdentityService).Assembly)); // Application
                c.IncludeXmlComments(GetXmlPathForAssembly(typeof(MatchTypeDto).Assembly)); // Contract

                // Authentication
                var scheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                };

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, scheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {   
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    } 
                });
            });
        }

        private static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = Constants.Auth.MinPasswordLength;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Constants.Auth.LockoutTimeSpan);
                options.Lockout.MaxFailedAccessAttempts = Constants.Auth.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.RequireUniqueEmail = false;
            });
        }

        private static void AddErrorHandling(IServiceCollection services)
        {
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
        }

        private void AddJwtAuthentication(IServiceCollection services)
        {
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
        }
    }
}
