using HealthESB.Framework.Utility;
using HealthESB.Infrastructure.Model.Channel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class AuthRequest: HealthESB.Infrastructure.Model.Channel.BaseApiRequest
    {
      //  [JsonProperty("username")]
        public string username { get; set; }
        //[JsonProperty("password")]
        public string password { get; set; }
       // [JsonProperty("terminalId")]
        public int terminalId { get; set; }
        public AuthRequest(string UserName,string Password,int TerminalId)
        {
            username = UserName;
            password = Password;
            terminalId = TerminalId;
        }
        
    }
}
