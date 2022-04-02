using AuthorizationServer.Extension;
using AuthorizationServer.Flows.Abstract;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace AuthorizationServer.Flows.TokenFlow.Concrete
{
    public class RefreshTokenFlow : ITokenFlow
    {

        public string GrantType => "refresh_token";

        public IClientManager _clientManager { get; }
        public ITokenService _tokenService { get; }

        public RefreshTokenFlow(IClientManager clientManager, ITokenService tokenService)
        {
            _clientManager = clientManager;
            _tokenService = tokenService;
        }


        public JsonResult ProcessFlow(TokenRequest tokenRequest)
        {
            if (tokenRequest.GrantType.IsNullOrWhiteSpace() || 
                tokenRequest.ClientSecret.IsNullOrWhiteSpace() || 
                tokenRequest.ClientId.IsNullOrWhiteSpace())
            {
                return FlowErrorResponse.InvalidRequest();
            }

            if (!_clientManager.IsValidClient(tokenRequest.ClientId, tokenRequest.ClientSecret))
            {
                return FlowErrorResponse.UnauthorizedClient();
            }

            if (!_clientManager.IsValidRefreshToken(tokenRequest.ClientId))
            {
                return FlowErrorResponse.UnauthorizedClient();
            }

            return  new JsonResult(_tokenService.GenerateToken(tokenRequest));
        }
    }
}



