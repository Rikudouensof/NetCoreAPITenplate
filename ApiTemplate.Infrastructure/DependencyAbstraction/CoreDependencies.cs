using ApiTemplate.Application.Helpers;
using ApiTemplate.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using ApiTemplate.Application.Services;
using ApiTemplate.Infrastructure.Services;


namespace ApiTemplate.Infrastructure.DependencyAbstraction
{
    public static class CoreDependencies
    {

        private static IAppsettingGeneratorHelper _appsettingGeneratorHelper;
        public static void Configure(IAppsettingGeneratorHelper appsettingGeneratorHelper)
        {
            _appsettingGeneratorHelper = appsettingGeneratorHelper;
        }

        public static IServiceCollection ImplementCoreDependencies(this IServiceCollection services)
        {
            var encryptionSetting = _appsettingGeneratorHelper.GenerateEncryption();
            var jwtSettings = _appsettingGeneratorHelper.GetJWTSettings();
            //Direct Dependency Integrations
            services.AddSingleton<ILoggerHelper, LoggerHelper>();
            services.AddSingleton<IGenerateJwtTokenHelper, GenerateJwtTokenHelper>();
            // services.AddScoped<IEncryptionHelper, EncryptionHelper>(provider => new EncryptionHelper(encryptionSetting.DirectEncyptPrivateKeyPath,encryptionSetting.DirectEncyptPublicKeyPath, _appsettingGeneratorHelper));
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            //Server Configuration
            services.PostConfigure<KestrelServerOptions>(options =>
            {
                options.AddServerHeader = false;
            });

            //Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.JWTIssuer,
                    ValidAudience = jwtSettings.JWTAudience,
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var jwtToken = securityToken as JwtSecurityToken;
                        if (jwtToken == null)
                        {
                            throw new SecurityTokenException("Invalid token");
                        }


                        // Resolve the key dynamically based on the token or user information
                        var username = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
                        var rsa = GeneralGeneratorHelpers.GenerateUserSpecificKey(username);
                        return new List<SecurityKey> { new RsaSecurityKey(rsa) };
                    }
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}