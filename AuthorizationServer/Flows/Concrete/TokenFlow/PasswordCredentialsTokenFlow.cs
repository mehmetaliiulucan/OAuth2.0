using AuthorizationServer.Extension;
using AuthorizationServer.Flows.Abstract;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;

namespace AuthorizationServer.Flows.TokenFlow.Concrete
{
    public class PasswordCredentialsTokenFlow : ITokenFlow
    {

        public string GrantType => "password";

        public IClientManager _clientManager { get; }
        public ITokenService _tokenService { get; }

        public PasswordCredentialsTokenFlow(IClientManager clientManager, ITokenService tokenService)
        {
            _clientManager = clientManager;
            _tokenService = tokenService;
        }


        public JsonResult ProcessFlow(TokenRequest tokenRequest)
        {

            if (tokenRequest.GrantType.IsNullOrWhiteSpace() || 
                tokenRequest.UserName.IsNullOrWhiteSpace() || 
                tokenRequest.Password.IsNullOrWhiteSpace())
            {
                return FlowErrorResponse.InvalidRequest();
            }

            if (!_clientManager.IsValidUser(tokenRequest.UserName, tokenRequest.Password))
            {
                return FlowErrorResponse.UnauthorizedClient();
            }

            return new JsonResult(_tokenService.GenerateToken(tokenRequest));
        }
    }
}



