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

        Task<BaseResponse> CreateAsync(ClaimsRow claimsRequest);
        Task<ClaimsResponse> GetListAsync(ListDTO listDTO);
        Task<BaseResponse> UpdateClaimsAsync(ClaimsRow claimsRequest);
        Task<ClaimsResponse> GetById(int Id);

    }
}
