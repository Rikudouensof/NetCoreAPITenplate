﻿using ApiTemplate.Application.Helpers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Infrastructure.Helpers
{
   

    public class GenerateJwtTokenHelper : IGenerateJwtTokenHelper
    {
        private IAppsettingGeneratorHelper _appsettingGeneratorHelper;
        public GenerateJwtTokenHelper(IAppsettingGeneratorHelper appsettingGeneratorHelper)
        {
            _appsettingGeneratorHelper = appsettingGeneratorHelper;
        }
        public string GenerateJwtToken(string username)
        {
            var jwtSetting = _appsettingGeneratorHelper.GetJWTSettings();
            var rsa = GeneralGeneratorHelpers.GenerateUserSpecificKey(username);
            var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtSetting.JWTIssuer,
                audience: jwtSetting.JWTAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
