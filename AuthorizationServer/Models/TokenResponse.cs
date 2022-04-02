using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace AuthorizationServer.Models
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public TimeSpan expires_in { get; set; }
        public string refresh_token { get; set; }

    }
}
