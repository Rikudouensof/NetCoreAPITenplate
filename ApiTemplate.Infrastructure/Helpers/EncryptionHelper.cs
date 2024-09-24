using ApiTemplate.Application.Helpers;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Infrastructure.Helpers
{
   
  
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly RSA _rsa;
        private IAppsettingGeneratorHelper _appsettingGenerator;
        public EncryptionHelper(string privateKeyPath, string publicKeyPath, IAppsettingGeneratorHelper appsettingGenerator)
        {
            _appsettingGenerator = appsettingGenerator;
            try
            {
                _rsa = RSA.Create();

                // Import the private key
                var privateKey = System.IO.File.ReadAllText(privateKeyPath);
                _rsa.ImportFromEncryptedPem(privateKey.ToCharArray(), "12345");

                // Import the public key
                var publicKey = System.IO.File.ReadAllText(publicKeyPath);
                _rsa.ImportFromPem(publicKey.ToCharArray());

             
            }
            catch (Exception ex)
            {

                
            }
           
        }

        public string EncryptPassword(string password)
        {
            // Generate a salt
            var salt = GenerateSalt();
            var saltedPassword = password + salt;

            // Encrypt the salted password using the public key
            var encryptedData = _rsa.Encrypt(Encoding.UTF8.GetBytes(saltedPassword), RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(encryptedData) + ":" + Convert.ToBase64String(salt);
        }

        public string DecryptPassword(string encryptedPassword)
        {
            var parts = encryptedPassword.Split(':');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid encrypted password format");

            var encryptedData = Convert.FromBase64String(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);

            // Decrypt the data using the private key
            var decryptedData = _rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
            var saltedPassword = Encoding.UTF8.GetString(decryptedData);

            // Remove the salt
            return saltedPassword.Substring(0, saltedPassword.Length - salt.Length);
        }

        private byte[] GenerateSalt()
        {
            var encryptionSettings = _appsettingGenerator.GenerateEncryption();
            // Use a specific date and time format
            string dateFormat = encryptionSettings.DelussionKey;
            string dateString = DateTime.UtcNow.ToString(dateFormat);

            // Convert the date string to bytes
            byte[] salt = Encoding.UTF8.GetBytes(dateString);

            // Ensure the salt is of a fixed length (e.g., 16 bytes)
            if (salt.Length < 16)
            {
                Array.Resize(ref salt, 16);
            }
            else if (salt.Length > 16)
            {
                Array.Resize(ref salt, 16);
            }

            return salt;
        }

    }
}

