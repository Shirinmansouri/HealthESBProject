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

namespace HealthESB.Domain.Service
{
    public class AvihangUserSessionsService : IAvihangUserSessionsService
    {
        private readonly FactoryChannel _factory;
        private IAvihangUserSessionsRepository _avihangUserSessionsRepository;
        private IProvidersRepository _providersRepository;
        private IProviderApisRepository _providerApisRepository;
        private ILogService _logService;
        private IAvihangTokensService _avihangTokensService;
        private IAvihangUserInfoRepository _avihangUserInfoRepository;
        public AvihangUserSessionsService(IAvihangUserInfoRepository avihangUserInfoRepository,IAvihangTokensService avihangTokensService,IAvihangUserSessionsRepository avihangUserSessionsRepository, ILogService logService, IProvidersRepository providersRepository, IProviderApisRepository providerApisRepository)
        {
            _avihangUserSessionsRepository = avihangUserSessionsRepository;
            _logService = logService;
            _factory = new RestApiAdabtorFactory().GetWebServiceFactory<FactoryChannel>();
            _providersRepository = providersRepository;
            _providerApisRepository = providerApisRepository;
            _avihangTokensService = avihangTokensService;
            _avihangUserInfoRepository = avihangUserInfoRepository;

        }
        private SessionState getSessionState(AvihangUserSessions avihangUserSessions)
        {
            if (avihangUserSessions == null)
                return Framework.Utility.SessionState.Empty;
            else
            {
                TimeSpan timeSpan = DateTime.Now - (DateTime)avihangUserSessions.UpdatedDate;
                if (timeSpan.TotalSeconds + 300 < (24) * 60 * 60)
                    return Framework.Utility.SessionState.Ok;
                else
                    return Framework.Utility.SessionState.Expired;
            }
        }
        public async  Task<BaseResponse> GetUserSession(int PartnerId, int CpartyId,int TerminalId)
        {
            AvihangUserSessions avihangUserSessions = new AvihangUserSessions();
            AvihangUserInfo avihangUserInfo = new AvihangUserInfo();
            BaseResponse baseResponse = new BaseResponse();                       
            avihangUserSessions = _avihangUserSessionsRepository.GetByPartnerIdAndCpartyId(PartnerId, CpartyId).Result;
            if (getSessionState(avihangUserSessions) != Framework.Utility.SessionState.Ok)
            {
                var providers = _providersRepository.GetById((int)ProviderType.Avihang).Result;
                var providerApis = _providerApisRepository.GetByProviderId((int)ProviderType.Avihang).Result;
                string ApiUrl = providerApis.Where(a => a.Id == (int)AvihangApis.InternalUserSession).FirstOrDefault().Url;
                string Uri = $"{(providers.BaseUrl.EndsWith("/") ? providers.BaseUrl : providers.BaseUrl + "/")}{(ApiUrl.StartsWith("/") ? ApiUrl.Remove(0, 1) : ApiUrl)}";
                Header header = new Header(Uri, 1, "application/json");
                header.token = _avihangTokensService.GetToken(TerminalId).Result.Token;
                header.clientAgentInfo = "172.16.2.9";
                header.clientIPAddress = "172.16.2.9";
                header.terminalId = TerminalId;
                UserSessionRequest userSessionRequest = new UserSessionRequest(PartnerId,CpartyId);
                var preResponse = _factory.GetChannel().CallWebApi<BaseApiHeader, UserSessionRequest>(header, userSessionRequest);
                avihangUserSessions = new AvihangUserSessions();
                avihangUserSessions.ResDate = DateTime.Now;
                baseResponse = Utilities.JsonTextToModel<BaseResponse>(preResponse.Content);
                baseResponse.CopyPropertiesTo(avihangUserSessions);
                baseResponse.info.CopyPropertiesTo(avihangUserSessions);
                baseResponse.info.CopyPropertiesTo(avihangUserInfo);
                await _avihangUserInfoRepository.Insert(avihangUserInfo);
                avihangUserSessions.AvihangUserInfoId = avihangUserInfo.Id;
                await _avihangUserSessionsRepository.Add(avihangUserSessions);
                await _avihangUserSessionsRepository.CommitAsync();

                
            }
            return baseResponse;
        }
    }
}
