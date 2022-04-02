using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Client : IBaseEntity
    {
        public Guid GrantId { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int TokenExpirationMin { get; set; } = 15;
        public bool IssueRefreshTokens { get; set; }
        public int RefreshTokenExpirationDays { get; set; } = 15;

        public string IssuerURI { get; set; }
        public string Audience { get; set; }

        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = true;

    }
}
