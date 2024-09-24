using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Infrastructure.Helpers
{
    public static class GeneralGeneratorHelpers
    {
        public static string GetNewRequestId()
        {
            Random rnd = new Random();
            int myRandomNo = rnd.Next(1000000, 9999999);
            var requestId = $"Track:{myRandomNo}";
            return requestId;
        }

        public static RSA GenerateUserSpecificKey(string input)
        {
            // Use user-specific data to generate a unique key
            var keyData = Encoding.UTF8.GetBytes(input);
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(keyData);

            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(hash, out _);
            return rsa;
        }
    }




}
