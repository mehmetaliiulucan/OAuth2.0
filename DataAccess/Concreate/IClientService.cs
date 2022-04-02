using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concreate
{
    public interface IClientService
    {
        public Client GetClient(string clientId);
    }
}
