using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Domain.Models.Configs;
using TaskManagement.Infrastructure.Middlewares;
using TaskManagement.Infrastructure.Persistance;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationSettings = new AuthenticationSettings();

            configuration.GetSection("JWTKey").Bind(authenticationSettings);

            services.AddControllers();

            services.AddSingleton(authenticationSettings);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("conString"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = authenticationSettings.ValidAudience,
                    ValidIssuer = authenticationSettings.ValidIssuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Secret))
                };
            });

            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITaskJobRepository, TaskJobRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("applicationCors", policy =>
                {
                    policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
        }
    }
}
