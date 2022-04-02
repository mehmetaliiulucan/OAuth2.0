using AuthorizationServer.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationServer.DataAccess
{
    public class ClientRepository : IClientRepository
    {
        private readonly List<Client> clients = new List<Client>
        {
            new Client() { ClientId = "client_id"},
            new Client() { ClientId = "client_id2"},
        };
        public Client GetClient(string clientId)
        {
            return clients.FirstOrDefault(x => x.ClientId == clientId);
        }

        public string GetClientScope(string clientId)
        {
            return "profile";
        }
    }
}
