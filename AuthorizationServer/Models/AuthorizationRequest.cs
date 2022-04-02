﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthorizationServer.Models
{
    public class AuthorizationRequest
    {
        [BindProperty(Name = "response_type")]
        public string ResponseType { get; set; }

        [BindProperty(Name = "redirect_uri")]
        public string RedirectUri { get; set; }

        [BindProperty(Name = "state")]
        public string State { get; set; }

        [BindProperty(Name = "client_id")]
        public string ClientId { get; set; }

        [BindProperty(Name = "scope")]
        public string Scope { get; set; }

    }
}
