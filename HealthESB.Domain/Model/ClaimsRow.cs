using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class ClaimsRow
    {
        public string ActionTitleFr { get; set; }
        public string ActionTitleEn { get; set; }
        public string ActionName { get; set; }
        public int ControllerEntityID { get; set; }
        public string ControllerName { get; set; }
        public string ControlleEnTitile { get; set; }
        public string ControlleFaTitile { get; set; }
        public int Id { get; set; }
    }
}
