
using HealthESB.Domain.Model.Avihang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface IAvihangUserSessionsService
    {
        Task<BaseResponse> GetUserSession(int PartnerId, int CpartyId, int TerminalId);
    }
}
