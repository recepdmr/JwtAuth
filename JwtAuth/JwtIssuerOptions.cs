using System;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuth
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public DateTime? NotBefore { get; internal set; }
        public DateTime? Expiration { get; internal set; }
    }
}
