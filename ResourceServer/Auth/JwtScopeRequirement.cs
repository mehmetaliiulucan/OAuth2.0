using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ResourceServer.Auth
{

    public class JwtScopeRequirement : IAuthorizationRequirement
    {
        public string Scope { get; }
        public JwtScopeRequirement(string scope)
        {
            Scope = scope;
        }
    }

    public class JwtScopeRequirementHandler : AuthorizationHandler<JwtScopeRequirement>
    {
        private readonly HttpClient _client;
        private readonly HttpContext _httpContext;

        public JwtScopeRequirementHandler(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _client = httpClientFactory.CreateClient();
            _httpContext = httpContextAccessor.HttpContext;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            JwtScopeRequirement requirement)
        {

            if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var accessToken = authHeader.ToString().Split(' ')[1];
                if (string.IsNullOrEmpty(accessToken))
                {
                    context.Fail();
                    return;
                }

                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await _client.GetAsync($"https://localhost:44358/token/validate");

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    context.Fail();
                    return;
                }

                var base64payload = GetBase64String(accessToken.Split('.')[1]);
                var bytes = Convert.FromBase64String(base64payload);
                var jsonPayload = Encoding.UTF8.GetString(bytes);
                var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPayload);
                var scopeClaims = claims.Where(x => x.Key == "scope");
                var hasClaim = scopeClaims.Any(x => x.Value == requirement.Scope);

                if (hasClaim)
                {
                    context.Succeed(requirement);
                }

            }
        }
        private string GetBase64String(string base64payload)
        {
            base64payload = base64payload.Replace('_', '/').Replace('-', '+');
            switch (base64payload.Length % 4)
            {
                case 2: base64payload += "=="; break;
                case 3: base64payload += "="; break;
            }

            return base64payload;
        }
    }

}
