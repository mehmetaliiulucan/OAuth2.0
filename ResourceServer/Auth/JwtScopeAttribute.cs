using Microsoft.AspNetCore.Authorization;

namespace ResourceServer.Auth
{
    public class JwtScopeAttribute : AuthorizeAttribute
    {
        public JwtScopeAttribute(string scope = null)
        {
            Policy = $"scope.{scope}";
        }
    }
}
