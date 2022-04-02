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
    public class JwtTokenService : ITokenService
    {
        public IClientRepository _clientRepository { get; }
        public ITokenGenerator _tokenGenerator { get; }

        public JwtTokenService(IClientRepository clientRepository, ITokenGenerator tokenGenerator)
        {
            _clientRepository = clientRepository;
            _tokenGenerator = tokenGenerator;
        }


        public TokenResponse GenerateToken(TokenRequest tokenRequest)
        {
            var client = _clientRepository.GetClient(tokenRequest.ClientId);

            var claims = new List<Claim>
{
                new Claim(JwtRegisteredClaimNames.Sub, tokenRequest.ClientId)
            };
            
            string scopes = _clientRepository.GetClientScope(tokenRequest.ClientId);
            if (!scopes.IsNullOrWhiteSpace())
            {
                claims.Add(new Claim("scope", scopes));
            }

            string accesToken = _tokenGenerator.GenerateToken(Startup.SECRET_KEY,
                                                              Startup.ISSUER,
                                                              Startup.AUDIANCE,
                                                              DateTime.Now,
                                                              DateTime.Now.AddMinutes(2),
                                                              claims);

            var tokenResponse = new TokenResponse
            {
                access_token = accesToken,
                token_type = "Barear",
                expires_in = TimeSpan.FromMinutes(2),
                refresh_token = GenerateRefreshToken(client.ClientId)
            };

            return tokenResponse;
        }

        private string GenerateRefreshToken(string clientId)
        {
            //Generate refresh token and save database
            return "SJDhsdjjedjfldlKDsdskdjsd";
        }
    }
}
