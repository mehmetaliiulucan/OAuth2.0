using AuthorizationServer.Models;

namespace AuthorizationServer.Flows.Abstract
{
    public interface ITokenService
    {
        public TokenResponse GenerateToken(TokenRequest tokenRequest);

    }
}
