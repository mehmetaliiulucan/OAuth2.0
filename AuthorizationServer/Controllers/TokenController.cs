using AuthorizationServer.Flows;
using AuthorizationServer.Flows.Concrete;
using AuthorizationServer.Flows.Abstract;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
namespace AuthorizationServer.Controllers
{
    public class TokenController : Controller
    {
        private readonly IEnumerable<ITokenFlow> _tokenFlows;

        public TokenController(IEnumerable<ITokenFlow> tokenFlows)
        {
            _tokenFlows = tokenFlows;
        }
        public ActionResult Token([FromForm] TokenRequest tokenRequest)
        {
            if(tokenRequest == null)
            {
                return FlowErrorResponse.InvalidRequest();
            }

            var tokenFlow = _tokenFlows.FirstOrDefault(x => x.GrantType == tokenRequest.GrantType);

            if(tokenFlow == null)
            {
                return FlowErrorResponse.InvalidGrant();
            }

            var val = tokenFlow.ProcessFlow(tokenRequest).Value;
            return Ok(val);
        }

        [Authorize]
        public IActionResult Validate()
        {
            return Ok();
        }
    }
}
