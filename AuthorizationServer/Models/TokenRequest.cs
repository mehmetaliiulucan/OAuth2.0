using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Models
{
    public class TokenRequest
    {
        [BindProperty(Name = "grant_type")]
        public string GrantType { get; set; }

        [BindProperty(Name = "redirect_uri")]
        public string RedirectUri { get; set; }

        [BindProperty(Name = "client_id")]
        public string ClientId { get; set; }

        [BindProperty(Name = "code")]
        public string Code { get; set; }

        [BindProperty(Name = "state")]
        public string State { get; set; }

        [BindProperty(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [BindProperty(Name = "client_secret")]
        public string ClientSecret { get; set; }
        [BindProperty(Name = "username")]
        public string UserName { get; set; }
        [BindProperty(Name = "password")]
        public string Password { get; set; }
    }
}
