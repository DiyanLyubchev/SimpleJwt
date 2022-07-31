using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sample.JWT.Token.Common;
using Sample.JWT.Token.Interfaces;
using Sample.JWT.Token.Managers;
using System.Text;

namespace Simple.JWT.Token.Extentions
{
    public static class ServiceCollectionExtensions
    {
        private static string key = JwtContainerModel.Key;

        public static IServiceCollection ResolveJwtConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));

            return services;
        }

        public static IServiceCollection ResolveSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample.JWT.Token", Version = "v1" });
            });

            return services;
        }

    }
}
