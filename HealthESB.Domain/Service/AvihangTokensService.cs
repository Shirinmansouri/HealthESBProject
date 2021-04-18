using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using HealthESB.Domain.Model.Avihang;
using HealthESB.Framework.Logger;
using HealthESB.Framework.Utility;
using HealthESB.Infrastructure.Channel;
using HealthESB.Infrastructure.Model.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HealthESB.Domain.Service
{
    public class AvihangTokensService : IAvihangTokensService
    {
        private readonly FactoryChannel _factory;
        private IAvihangTokensRepository _avihangTokensRepository;
        private IProvidersRepository _providersRepository;
        private IProviderApisRepository _providerApisRepository;
        private ILogService _logService;
        public AvihangTokensService(IAvihangTokensRepository avihangTokensRepository, ILogService logService, IProvidersRepository providersRepository, IProviderApisRepository providerApisRepository)
        {
            _avihangTokensRepository = avihangTokensRepository;
            _logService = logService;
            _factory = new RestApiAdabtorFactory().GetWebServiceFactory<FactoryChannel>();
            _providersRepository = providersRepository;
            _providerApisRepository = providerApisRepository;



        }

        private TokenState TokenState(AvihangTokens avihangTokens)
        {
            if (avihangTokens == null)
                return Framework.Utility.TokenState.Empty;
            else
            {
                TimeSpan timeSpan = DateTime.Now - (DateTime)avihangTokens.UpdatedDate;
                if (timeSpan.TotalSeconds + 300 < (avihangTokens.ttl) * 60 * 60)
                    return Framework.Utility.TokenState.Ok;
                else
                    return Framework.Utility.TokenState.Expired;
            }
        }
        public async Task<AvihangTokens> GetToken(int terminalId)
        {
            AuthResponse authResponse = new AuthResponse();
            AvihangTokens avihangTokens = new AvihangTokens();
            try
            {
            
                  avihangTokens = _avihangTokensRepository.GetByTerminalId(terminalId.ToString()).Result;
                if (TokenState(avihangTokens) != Framework.Utility.TokenState.Ok)
                {
                    var providers = _providersRepository.GetById((int)ProviderType.Avihang).Result;
                    var providerApis = _providerApisRepository.GetByProviderId((int)ProviderType.Avihang).Result;
                    string ApiUrl = providerApis.Where(a => a.Id == (int)AvihangApis.Token).FirstOrDefault().Url;
                    string Uri = $"{(providers.BaseUrl.EndsWith("/") ? providers.BaseUrl : providers.BaseUrl + "/")}{(ApiUrl.StartsWith("/") ? ApiUrl.Remove(0, 1) : ApiUrl)}";
                    BaseApiHeader header = new BaseApiHeader(Uri, 1, "application/json");
                    AuthRequest authRequest = new AuthRequest(providers.UserName, providers.Password, terminalId);
                    var preResponse = _factory.GetChannel().CallWebApi<BaseApiHeader, AuthRequest>(header, authRequest);
                    avihangTokens.ResDate = DateTime.Now;
                    authResponse = Utilities.JsonTextToModel<AuthResponse>(preResponse.Content);                  
                    authResponse.CopyPropertiesTo(avihangTokens);
                    authResponse.info.CopyPropertiesTo(avihangTokens);
                    avihangTokens.TerminalId = terminalId.ToString();
                    authResponse.info.terminalId = terminalId;
                    await _avihangTokensRepository.Add(avihangTokens);
                    await _avihangTokensRepository.CommitAsync();


                }
                
            }
            catch (Exception ex)
            {
                _logService.LogText(ex.Message);
                throw;
            }


            return avihangTokens;


        }
    }
}
