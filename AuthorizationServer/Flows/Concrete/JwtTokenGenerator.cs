using AuthorizationServer.DataAccess;
using AuthorizationServer.Extension;
using AuthorizationServer.Flows.Abstract;
using AuthorizationServer.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizationServer.Flows.Concrete
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        public string GenerateToken(string secret, string issuer, string audiance, DateTime nbf, DateTime expires,  List<Claim> claims)
        {
           
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                issuer,
                audiance,
                claims,
                notBefore: nbf,
                expires: expires,
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
