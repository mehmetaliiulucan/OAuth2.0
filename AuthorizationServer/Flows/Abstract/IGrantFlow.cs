using AuthorizationServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthorizationServer.Flows.Abstract
{
    public interface IGrantFlow
    {
        string ResponseType { get; }
        public string ProcessFlow(LoginModel request);
        public (bool, string) Validate(AuthorizationRequest authorizationRequest);

    }
}
