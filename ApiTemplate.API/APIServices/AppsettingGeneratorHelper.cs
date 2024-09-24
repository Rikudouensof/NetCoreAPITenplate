using ApiTemplate.Application.Helpers;
using ApiTemplate.Domain.Models.CoreModels;

namespace ApiTemplate.API.APIServices
{
    public class AppsettingGeneratorHelper : IAppsettingGeneratorHelper
    {

        private readonly IConfiguration _config;
        public AppsettingGeneratorHelper(IConfiguration configuration)
        {
            _config = configuration;
        }
        public AppSettings GenerateAppSettings()
        {
            var appsetting = new AppSettings()
            {
                EncryptPrivateKeyPath = "To be removed"
            };
            return appsetting;
        }

        public EncryptionSettings GenerateEncryption()
        {
            var encyptionSettings = new EncryptionSettings()
            {
                DelussionKey = _config["EncryptionSettings:DelussionKey"],
                DirectEncyptPrivateKeyPath = _config["EncryptionSettings:DirectEncyptPrivateKeyPath"],
                JWTKey = _config["EncryptionSettings:JWTKey"],
                JWTKeyPath = _config["EncryptionSettings:JWTKeyPath"],
                DirectEncyptPublicKeyPath = _config["EncryptionSettings:DirectEncyptPublicKeyPath"],
                SaltKey = _config["EncryptionSettings:SaltKey"]
            };
            return encyptionSettings;
        }


        public JWTSettings GetJWTSettings()
        {
            var jwtSettings = new JWTSettings()
            {
                JWTAudience = _config["JwtSettings:Audience"],
                JWTIssuer = _config["JwtSettings:Issuer"]
            };
            return jwtSettings;
        }
    }
}
