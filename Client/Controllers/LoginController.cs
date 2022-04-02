using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client;
        public IConfiguration _configuration { get; }

        //public LoginController(IHttpClientFactory httpClientFactory)
        //{
        //    _client = httpClientFactory.CreateClient();
        //}
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginWithMyApp()
        {
            string redirectUrl = _configuration.GetValue<string>("MyAppInfo:AuthorizeUrl");
            var query = new QueryBuilder();
            query.Add("response_type", "code");
            query.Add("client_id", _configuration.GetValue<string>("MyAppInfo:ClientId"));
            query.Add("redirect_uri", _configuration.GetValue<string>("MyAppInfo:RedirectUrl"));
            query.Add("state", "deneme123");

            return Redirect($"{redirectUrl}{query.ToString()}");
        }

        [HttpGet]
        public IActionResult ExternalLoginCallBack()
        {
            var query = new QueryBuilder();
            query.Add("code", "");

            return View();
        }
    }
}
