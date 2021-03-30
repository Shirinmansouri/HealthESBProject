using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using HealthESB.Domain.Model;
using HealthESB.Framework.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Service
{
    public class AspNetUserRolesService : IAspNetUserRolesService

    {
        private ILogService _logService;
        private IAspNetUserRolesRespository _aspNetUserRolesRespository;
        public AspNetUserRolesService(IAspNetUserRolesRespository aspNetUserRolesRespository, ILogService logService)
        {
            _logService = logService;
            _aspNetUserRolesRespository = aspNetUserRolesRespository;
        }
        public async Task<RoleListResponse> getUserRolesByUserIdAsync(string UserId)
        {
            return await _aspNetUserRolesRespository.getUserRolesByUserIdAsync(UserId);
        }
        public async Task<ClaimsResponse> getUserClaimsByUserIdAsync(string UserId)
        {
            return await _aspNetUserRolesRespository.getUserClaimsByUserIdAsync(UserId);
        }
        public async Task<UserListResponse> getUsersAsync(ListDTO listDTO)
        {
            return await _aspNetUserRolesRespository.getUsersAsync(listDTO);
        }
    }
}
