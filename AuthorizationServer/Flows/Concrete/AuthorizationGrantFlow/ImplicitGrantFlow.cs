using AuthorizationServer.Extension;
using AuthorizationServer.Flows.Abstract;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace AuthorizationServer.Flows.Concrete.AuthorizationGrantFlow
{
    public class ImplicitGrantFlow : IGrantFlow
    {
        public string ResponseType => "token";

        public IClientManager _clientManager { get; }
        ITokenService _tokenService { get; }
        public ImplicitGrantFlow(IClientManager clientManager, ITokenService tokenService)
        {
            _clientManager = clientManager;
            _tokenService = tokenService;

        }

        public string ProcessFlow(LoginModel model)
        {
            var (error, errorResult) = Validate(model);
            
            if (error)
            {
                return errorResult;
            }

            TokenRequest tokenRequest = new TokenRequest
            {
                ClientId = model.ClientId
            };

            var query = new QueryBuilder();

            var token = _tokenService.GenerateToken(tokenRequest);

            query.Add("access_token", token.access_token);
            query.Add("expires_in", token.expires_in.ToString());

            if (!model.State.IsNullOrWhiteSpace())
            {
                query.Add("state", model.State);
            }


            return $"{model.RedirectUri}{query.ToString()}";
        }

        public (bool, string) Validate(AuthorizationRequest authorizationRequest)
        {
            return Validate(authorizationRequest.ResponseType, authorizationRequest.ClientId, authorizationRequest.RedirectUri, authorizationRequest.Scope);
        }

        private (bool, string) Validate(LoginModel model)
        {
            return Validate(model.ResponseType, model.ClientId, model.RedirectUri, model.Scope);
        }
        private (bool, string) Validate(string responseType, string clientId, string redirectUri, string scope)
        {
            if (responseType.IsNullOrWhiteSpace() || clientId.IsNullOrWhiteSpace() || redirectUri.IsNullOrWhiteSpace())
            {
                return (true, FlowErrorResponse.InvalidRequest(redirectUri, null));
            }

            if (!_clientManager.IsValidClient(clientId))
            {
                return (true, FlowErrorResponse.UnauthorizedClient(redirectUri, null));
            }

            if (!_clientManager.IsRedirectURIValid(clientId, redirectUri))
            {
                return (true, FlowErrorResponse.UnauthorizedClient(redirectUri, null));
            }

            if (!_clientManager.IsGrantValid(clientId, responseType))
            {
                return (true, FlowErrorResponse.UnauthorizedClient(redirectUri, null));
            }

            if (!_clientManager.IsScopeValid(clientId, scope))
            {
                return (true, FlowErrorResponse.InvalidScope(redirectUri, null));
            }

            return (false, null);

        }

    }
}



