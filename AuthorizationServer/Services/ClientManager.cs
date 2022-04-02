namespace AuthorizationServer.Services
{
    public class ClientManager : IClientManager
    {
        public bool IsAuthorizationCodeValid(string clientId, string authCode)
        {
            return true;
        }
        public bool IsValidClient(string clientId, string clientSecret)
        {
            return true;
        }
        public bool IsGrantValid(string clientId, string grantType)
        {
            return true;
        }

        public bool IsRedirectURIValid(string clientId, string redirectUri)
        {
            return true;
        }

        public bool IsScopeValid(string clientId, string scope)
        {
            return true;
        }

        public bool IsValidClient(string clientId)
        {
            return true;
        }

        public bool IsValidUser(string clientId, string clientSecret)
        {
            return true;
        }

        public bool IsValidRefreshToken(string clientId)
        {
            return true;
        }
    }
}
