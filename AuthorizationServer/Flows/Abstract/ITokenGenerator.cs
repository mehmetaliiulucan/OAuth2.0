using AuthorizationServer.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthorizationServer.Flows.Abstract
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string secret, string issuer, string audiance, DateTime nbf, DateTime expires,  List<Claim> claims);

    }
}
