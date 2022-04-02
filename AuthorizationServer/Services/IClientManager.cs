namespace AuthorizationServer.Services
{
    public interface IClientManager
    {
        bool IsValidClient(string clientId);
        bool IsValidClient(string clientId, string clientSecret);
        bool IsGrantValid(string clientId, string grantType);
        bool IsScopeValid(string clientId, string scope);
        bool IsRedirectURIValid(string clientId, string redirectUri);
        bool IsAuthorizationCodeValid(string clientId, string authCode);
        bool IsValidUser(string clientId, string clientSecret);
        bool IsValidRefreshToken(string clientId);

    }
}
