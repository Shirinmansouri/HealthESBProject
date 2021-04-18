using HealthESB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.EF.Data
{
    public static class ProvidersData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var LstProviders = new List<Providers>();
            var Child1 = new Providers()
            {
                Id = 1,
                BaseUrl = "http://api.ttac.ir/insurances/test/v74/",
                Name = "سازمان غذا و دارو",
                UserName = "bimeh_salamat",
                Password = "ghkZKzBwdy"

            };
            LstProviders.Add(Child1);
            var Child2 = new Providers()
            {
                Id = 2,
                BaseUrl = "http://test.ihio.gov.ir/erx-core/v3/service/",
                Name = "سرویس های نسخه الکترونیک",
                UserName = "HDK_hasanlou_test",
                Password = "9123085676"

            };
            LstProviders.Add(Child2);
            modelBuilder.Entity<Providers>().HasData(LstProviders);
            
        }
    }
}
