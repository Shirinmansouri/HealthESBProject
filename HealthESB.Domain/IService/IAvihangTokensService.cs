using HealthESB.Domain.Entities;
using HealthESB.Domain.Model.Avihang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface IAvihangTokensService
    {
 
       Task<AvihangTokens> GetToken(int terminalId);
    }
}
