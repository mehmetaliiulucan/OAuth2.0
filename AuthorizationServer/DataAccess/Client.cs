using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationServer.DataAccess.Models
{
    public class Client 
    {
        public string ClientId { get; set; }
        public string Scopes { get; set; }
    }
}
