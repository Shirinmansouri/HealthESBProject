using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class Message
    {
        public List<SnackMessage> snackMessages { get; set; }
        public List<Info> infoMessages { get; set; }
    }
}
