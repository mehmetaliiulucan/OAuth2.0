using AuthorizationServer.Extension;
using AuthorizationServer.Flows;
using AuthorizationServer.Flows.Concrete;
using AuthorizationServer.Flows.Abstract;
using AuthorizationServer.Models;
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
using System;
using Newtonsoft.Json.Linq;
using System.Web;

namespace AuthorizationServer.Controllers
{
    public class OauthController : Controller
    {
        private readonly IEnumerable<IGrantFlow> _grantFlows;

        public OauthController(IEnumerable<IGrantFlow> grantFlows)
        {
            _grantFlows = grantFlows;
        }


        [HttpGet]
        public IActionResult Authorize([FromQuery] AuthorizationRequest authorizationRequest)
        {

            if (authorizationRequest == null)
            {
                return Redirect(FlowErrorResponse.InvalidRequest(authorizationRequest.RedirectUri, null));
            }

            var grantFlow = _grantFlows.FirstOrDefault(x => x.ResponseType == authorizationRequest.ResponseType);

            if (grantFlow == null)
            {
                return Redirect(FlowErrorResponse.UnsupportedResponseType(authorizationRequest.RedirectUri, null));
            }

            var (error, errorResult) = grantFlow.Validate(authorizationRequest);

            if (error)
            {
                return Redirect(errorResult);
            }

            LoginModel model = new LoginModel
            {
                RedirectUri = authorizationRequest.RedirectUri,
                ResponseType = authorizationRequest.ResponseType,
                ClientId = authorizationRequest.ClientId,
                State = authorizationRequest.State
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Authorize(LoginModel loginModel)
        {
            if (loginModel.Username.IsNullOrWhiteSpace() || loginModel.Password.IsNullOrWhiteSpace())
            {
                return BadRequest("Username and password are required");
            }

            if (loginModel.Username != "mehmet" || loginModel.Password != "ulucan")
            {
                return BadRequest("Check your username and password.");
            }

            var grantFlow = _grantFlows.FirstOrDefault(x => x.ResponseType == loginModel.ResponseType);

            return Redirect(grantFlow.ProcessFlow(loginModel));
        }
    }
}
