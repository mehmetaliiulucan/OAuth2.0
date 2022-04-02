using AuthorizationServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthorizationServer.Flows.Abstract
{
    public interface ITokenFlow
    {
        string GrantType { get; }
        public JsonResult ProcessFlow(TokenRequest tokenRequest);
    }
}
