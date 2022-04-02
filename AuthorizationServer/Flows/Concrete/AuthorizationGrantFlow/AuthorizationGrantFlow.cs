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
    public class AuthorizationGrantFlow : IGrantFlow
    {
        public string ResponseType => "code";

        public IClientManager _clientManager { get; }

        public AuthorizationGrantFlow(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }
        public string ProcessFlow(LoginModel model)
        {
            var (error, errorResult) = Validate(model);

            if (error)
            {
                return errorResult;
            }

            var authCode = GenerateAuthorizationCode(model.ClientId);
            var query = new QueryBuilder();
            query.Add("code", authCode);

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
            var responseType = model.ResponseType;
            var clientId = model.ClientId;
            var redirectUri = model.RedirectUri;
            var scope = model.Scope;

            return Validate(responseType, clientId, redirectUri, scope);
        }
        private (bool, string) Validate(string responseType, string clientId, string redirectUri, string scope)
        {
            if (responseType.IsNullOrWhiteSpace() || clientId.IsNullOrWhiteSpace())
            {
                return (true, FlowErrorResponse.InvalidRequest(redirectUri, null));
            }

            if (!_clientManager.IsValidClient(clientId))
            {
                return (true, FlowErrorResponse.UnauthorizedClient(redirectUri, null));
            }

            if (!_clientManager.IsGrantValid(clientId, responseType))
            {
                return (true, FlowErrorResponse.UnauthorizedClient(redirectUri, null));
            }

            if (!_clientManager.IsRedirectURIValid(clientId, redirectUri))
            {
                return (true, FlowErrorResponse.UnauthorizedClient(redirectUri, null));
            }

            if (!_clientManager.IsScopeValid(clientId, scope))
            {
                return (true, FlowErrorResponse.InvalidScope(redirectUri, null));
            }

            return (false, null);

        }
        private string GenerateAuthorizationCode(string clientId)
        {
            //Generate auth code and save db
            return $"DENEMEAMACLIYAPILDI";
        }
    }
}



