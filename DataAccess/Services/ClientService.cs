using DataAccess.Concreate;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Services
{
    public class ClientService : IClientService
    {
        public Client GetClient(string clientId)
        {
            return new Client();
        }
    }
}
