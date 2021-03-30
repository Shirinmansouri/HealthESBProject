using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthESB.Domain.IService;
using HealthESB.Domain.Service;
using HealthESB.Domain.IRepository;
using HealthESB.Persistance.Repository;
using HealthESB.Framework.Logger;
namespace HealthESB.Framework.DependecyConfig
{
    public class ConfigureServices
    {
        public ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPrescriptionBarcodeService, PrescriptionBarcodeService>();
            services.AddTransient<IPrescriptionBarcodeDetailesService, PrescriptionBarcodeDetailesService>();
            services.AddTransient<IPrescriptionBarcodeDetailesRepository, PrescriptionBarcodeDetailesRepository>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();
            services.AddTransient<IPrescriptionBarcodeRepository, PrescriptionBarcodeRepository>();
            services.AddTransient<IPrescriptionRepository, PrescriptionRepository>();
            services.AddTransient<ILogService,LogService>();
            services.AddTransient<IAspNetUserRolesRespository, AspNetUserRolesRespository>();
            services.AddTransient<IAspNetUserRolesService, AspNetUserRolesService>();
            services.AddTransient<IClaimsRepository, ClaimsRepository>();
            services.AddTransient<IClaimsService, ClaimsService>();
        }
    }
}
