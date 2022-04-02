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
    public class ClientCredentialsTokenFlow : ITokenFlow
    {

        public string GrantType => "client_credentials";

        public IClientManager _clientManager { get; }
        public ITokenService _tokenService { get; }
        public HttpContext _httpContext { get; }

        public ClientCredentialsTokenFlow(IClientManager clientManager, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _clientManager = clientManager;
            _tokenService = tokenService;
            _httpContext = httpContextAccessor.HttpContext;
        }


        public JsonResult ProcessFlow(TokenRequest tokenRequest)
        {
            if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var base64ClientInfo = authHeader.ToString().Split(' ')[1];
                var bytes = Convert.FromBase64String(base64ClientInfo);
                var clientInfoString = Encoding.UTF8.GetString(bytes);

                string[] clientInfo = clientInfoString.Split(':');
                string clientId = clientInfo[0];
                string clientSecret = clientInfo[1];

                if (!_clientManager.IsValidClient(clientId, clientSecret))
                {
                    return FlowErrorResponse.UnauthorizedClient();
                }

                tokenRequest.ClientId = clientId;
                tokenRequest.ClientSecret = clientSecret;
               
                return new JsonResult(_tokenService.GenerateToken(tokenRequest));

            }

            return FlowErrorResponse.InvalidRequest();

        }
    }
}



