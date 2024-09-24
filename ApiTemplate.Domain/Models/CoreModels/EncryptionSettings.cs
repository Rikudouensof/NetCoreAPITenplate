using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.Models.CoreModels
{
    public class EncryptionSettings
    {
        public string? DirectEncyptPrivateKeyPath { get; set; } = string.Empty;
        public string? DirectEncyptPublicKeyPath { get; set; } = string.Empty;
        public string? JWTKey { get; set; } = string.Empty;
        public string? JWTKeyPath { get; set; } = string.Empty;
        public string? SaltKey { get; set; } = string.Empty;
        public string? DelussionKey { get; set; } = string.Empty;
    }
}
