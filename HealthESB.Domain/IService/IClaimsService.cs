using HealthESB.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface IClaimsService
    {

         Task<BaseResponse> CreateAsync(ClaimsRequest claimsRequest);
         Task<ClaimsResponse> GetListAsync(ListDTO listDTO);
         Task<BaseResponse> UpdateClaimsAsync(ClaimsRequest claimsRequest);

    }
}
