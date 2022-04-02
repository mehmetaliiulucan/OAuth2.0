using AuthorizationServer.DataAccess.Models;

namespace AuthorizationServer.DataAccess
{
    public interface IClientRepository
    {
        public Client GetClient(string clientId);
        public string GetClientScope(string clientId);
    }
}
