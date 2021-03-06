using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class ConfirmRequest
    {
        public List<UidInfo> Uids { get; set; }//لیستی از کدهای اصالت
    }
    public class UidInfo
    {
        public int Count { get; set; }//تعداد دارو
        public string ReCheckCode { get; set; } = "";//کد اختصاصی قلم دارو
    }
}
