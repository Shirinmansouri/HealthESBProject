using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class ListDTO : BaseRequest
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string Filter { get; set; }
        public bool IsRequestCount { get; set; }
        //public List<FilterRow> FilterLst { get; set; }
    }
}
