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
using HealthESB.Infrastructure.IChannel;
using HealthESB.Infrastructure.Channel;
using HealthESB.Framework.Utility;
using HealthESB.EF;

namespace HealthESB.Framework.DependecyConfig
{
    public class ConfigureServices
    {
        public ConfigureServices(IServiceCollection services)
        {
           // services.AddDbContext<HealthESBDbContext>(ServiceLifetime.Transient);
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
            services.AddTransient<IAvihangCitizenSessionsService, AvihangCitizenSessionsService>();
            services.AddTransient<IAvihangPersonInfoService, AvihangPersonInfoService>();
            services.AddTransient<IAvihangPrescriptionsService, AvihangPrescriptionsService>();
            services.AddTransient<IAvihangPrescriptionSubscriptionsService, AvihangPrescriptionSubscriptionsService>();
            services.AddTransient<IAvihangSamadsService, AvihangSamadsService>();
            services.AddTransient<IAvihangSnackMessageService, AvihangSnackMessageService>();
            services.AddTransient<IAvihangSubsciptionsService, AvihangSubsciptionsService>();
            services.AddTransient<IAvihangTokensService, AvihangTokensService>();
            services.AddTransient<IAvihangUserInfoService, AvihangUserInfoService>();
            services.AddTransient<IAvihangUserSessionsService, AvihangUserSessionsService>();
            services.AddTransient<IAvihangCitizenSessionsRepository, AvihangCitizenSessionsRepository>();
            services.AddTransient<IAvihangPersonInfoRepository, AvihangPersonInfoRepository>();
            services.AddTransient<IAvihangPrescriptionsRepository, AvihangPrescriptionsRepository>();
            services.AddTransient<IAvihangPrescriptionSubscriptionsRepository, AvihangPrescriptionSubscriptionsRepository>();
            services.AddTransient<IAvihangSamadsRepository, AvihangSamadsRepository>();
            services.AddTransient<IAvihangSnackMessageRepository, AvihangSnackMessageRepository>();
            services.AddTransient<IAvihangSubsciptionsRepository, AvihangSubsciptionsRepository>();
            services.AddTransient<IAvihangTokensRepository, AvihangTokensRepository>();
            services.AddTransient<IAvihangUserInfoRepository, AvihangUserInfoRepository>();
            services.AddTransient<IAvihangUserSessionsRepository, AvihangUserSessionsRepository>();
            services.AddTransient<IProviderApisRepository, ProviderApisRepository>();
            services.AddTransient<IProviderApisService, ProviderApisService>();
            services.AddTransient<IProvidersService, ProvidersService>();
            services.AddTransient<IProvidersRepository, ProvidersRepository>();
            services.AddTransient<IKeyLockerStore, KeyLockerStore>();
            services.AddTransient<MemoryCacheService>();
            services.AddTransient<RedisCacheService>();
            services.AddTransient<Func<CacheTech, ICacheService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case CacheTech.Memory:
                        return serviceProvider.GetService<MemoryCacheService>();
                    case CacheTech.Redis:
                        return serviceProvider.GetService<RedisCacheService>();
                    default:
                        return serviceProvider.GetService<MemoryCacheService>();
                }
            });
        }
    }
}
