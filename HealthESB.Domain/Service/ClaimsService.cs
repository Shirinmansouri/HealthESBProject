using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using HealthESB.Domain.Model;
using HealthESB.Framework.Logger;
using HealthESB.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Service
{
    public class ClaimsService : IClaimsService
    {
        private IClaimsRepository _claimsRepository;
        private ILogService _logService;
        public ClaimsService(IClaimsRepository claimsRepository, ILogService logService)
        {
            _claimsRepository = claimsRepository;
            _logService = logService;


        }
        public async Task<BaseResponse> CreateAsync(ClaimsRequest claimsRequest)
        {
            try
            {
                var response = new BaseResponse();
                Claims claims = new Entities.Claims() { Name = claimsRequest.Name, Value = claimsRequest.Value };
                await _claimsRepository.Add(claims);
                return response.ToSuccess<BaseResponse>();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
           

        }
        public async Task<ClaimsResponse> GetListAsync(ListDTO listDTO)
        {
            var response = new ClaimsResponse();
            try
            {
                response = await _claimsRepository.GetClaimListByPaging(listDTO);
                return response.ToSuccess<ClaimsResponse>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<BaseResponse> UpdateClaimsAsync(ClaimsRequest claimsRequest)
        {
            try
            {
                var response = new BaseResponse();
                // var result =await  _claimsRepository.FirstOrDefault(a => a.Id == claimsRequest.Id);
                Claims result = new Claims();
                result.Name = claimsRequest.Name;
                result.Value = claimsRequest.Value;
                result.Id = claimsRequest.Id;
                await _claimsRepository.Update(result);
                return response.ToSuccess<BaseResponse>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}