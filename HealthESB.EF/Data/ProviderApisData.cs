using HealthESB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.EF.Data
{
    public static class ProviderApisData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var LstProviderApis = new List<ProviderApis>();
            var child1 = new ProviderApis()
            {
                Id = 1,
                Key = "ec279046-7f84-428d-ac8a-e7a3a7305a14",
                ProviderId = 1,
                Name = "ثبت نسخه",
                Url = "registerprescription"


            };
            LstProviderApis.Add(child1);
            var child2 = new ProviderApis()
            {
                Id = 2,
                Key = "ac89bf71-844c-4d5c-8472-185ee0413d61",
                ProviderId = 1,
                Name = "ثبت داروهای نسخه",
                Url = "checksinglebarcodeuid"


            };
            LstProviderApis.Add(child2);
            var child3 = new ProviderApis()
            {
                Id = 3,
                Key = "69869aa7-c493-48eb-acf4-bd21f186672b",
                ProviderId = 1,
                Name = "فعال سازی مجدد ",
                Url = "reactivateuid"


            };
            LstProviderApis.Add(child3);
            var child4 = new ProviderApis()
            {
                Id = 4,
                Key = "",
                ProviderId = 2,
                Name = "وب سرویس واکشی توکن",
                Url = "auth/token/fetch"


            };
            LstProviderApis.Add(child4);
            var child5= new ProviderApis()
            {
                Id = 5,
                Key = "",
                ProviderId = 2,
                Name = "وب سرویس ایجاد نشست کاربر",
                Url = "internal/session/cparty/open"


            };
            LstProviderApis.Add(child5);
            var child6 = new ProviderApis()
            {
                Id = 6,
                Key = "",
                ProviderId = 2,
                Name = "وب سرویس ایجاد نشست شهروند",
                Url = "auth/session/citizen/open"


            };
            LstProviderApis.Add(child6);
            var child7= new ProviderApis()
            {
                Id = 7,
                Key = "",
                ProviderId = 2,
                Name = "وب سرویس ایجاد کد سماد الکترونیک",
                Url = "samad/electronic/generate"


            };
            LstProviderApis.Add(child7);
            var child8= new ProviderApis()
            {
                Id = 8,
                Key = "",
                ProviderId = 2,
                Name = "وب سرویس ثبت نسخه",
                Url = "prescription/save"


            };
            LstProviderApis.Add(child8);
            var child9 = new ProviderApis()
            {
                Id = 9,
                Key = "",
                ProviderId = 2,
                Name = "وب سرویس بررسی ریز نسخه تجویز",
                Url = "subscription/check/order"


            };
            LstProviderApis.Add(child9);
            modelBuilder.Entity<ProviderApis>().HasData(LstProviderApis);
       
        }
    }
 

  }