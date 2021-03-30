using HealthESB.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface IAspNetUserRolesService
    {
        Task<RoleListResponse> getUserRolesByUserIdAsync(string UserId);
        Task<UserListResponse> getUsersAsync(ListDTO listDTO);
        Task<ClaimsResponse> getUserClaimsByUserIdAsync(string UserId);
    }
}
