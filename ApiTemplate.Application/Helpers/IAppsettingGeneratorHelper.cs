using ApiTemplate.Domain.Models.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Helpers
{
    public interface IAppsettingGeneratorHelper
    {

        EncryptionSettings GenerateEncryption();

        AppSettings GenerateAppSettings();

        JWTSettings GetJWTSettings();
    }
}
