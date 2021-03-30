using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class UserListResponse : BaseResponse
    {
        public List<UserRow> Users { get; set; }
        public int LstCount { get; set; }

    }
}
