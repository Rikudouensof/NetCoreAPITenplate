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
            try
            {
                // Use user-specific data to generate a unique key
                var keyData = Encoding.UTF8.GetBytes(input);
                using var sha256 = SHA256.Create();
                var hash = sha256.ComputeHash(keyData);

                var rsa = RSA.Create();
                rsa.KeySize = 2048;

                // Generate a new RSA key pair
                var parameters = rsa.ExportParameters(true);

                // Ensure the Exponent and Modulus are set
                parameters.Exponent = new byte[] { 1, 0, 1 }; // Common public exponent
                parameters.Modulus = hash.Take(256).ToArray(); // Ensure the modulus is the correct length

                // Generate private key parameters
                var rng = new Random(BitConverter.ToInt32(hash, 0));
                parameters.D = new byte[256];
                parameters.P = new byte[128];
                parameters.Q = new byte[128];
                parameters.DP = new byte[128];
                parameters.DQ = new byte[128];
                parameters.InverseQ = new byte[128];

                rng.NextBytes(parameters.D);
                rng.NextBytes(parameters.P);
                rng.NextBytes(parameters.Q);
                rng.NextBytes(parameters.DP);
                rng.NextBytes(parameters.DQ);
                rng.NextBytes(parameters.InverseQ);

                // Ensure the lengths of the parameters are correct
                if (parameters.D.Length != parameters.Modulus.Length ||
                    parameters.P.Length != parameters.Modulus.Length / 2 ||
                    parameters.Q.Length != parameters.Modulus.Length / 2 ||
                    parameters.DP.Length != parameters.Modulus.Length / 2 ||
                    parameters.DQ.Length != parameters.Modulus.Length / 2 ||
                    parameters.InverseQ.Length != parameters.Modulus.Length / 2)
                {
                    throw new InvalidOperationException("RSA parameters are not valid.");
                }

                rsa.ImportParameters(parameters);

                return rsa;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error generating RSA key: {ex.Message}");
                throw new InvalidOperationException("Error generating RSA key.", ex);
            }

        }
    }




}
