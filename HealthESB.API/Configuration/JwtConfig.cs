using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace HealthESB.API.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public  int TokenExpirationHours { get; set; }

    }
}
