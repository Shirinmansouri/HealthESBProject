using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.Model;
using HealthESB.EF;
using HealthESB.EF.DynamicFilter;
using HealthESB.Framework.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Persistance.Repository
{
    public class AspNetUserRolesRespository : GenericRepository<AspNetUserRoles, HealthESBDbContext>, IAspNetUserRolesRespository
    {
        public AspNetUserRolesRespository(HealthESBDbContext context) : base(context)
        {
        }
        public async Task<RoleListResponse> getUserRolesByUserIdAsync(string UserId)
        {

            var lst = Context.UserRoles.Where(a => a.UserId == UserId).AsQueryable();
            var result = Context.Roles.Join(lst, a => a.Id, b => b.RoleId, (a, b) => new RoleRow { Name = a.Name, Id = a.Id }).AsQueryable();
            RoleListResponse rowListResponse = new RoleListResponse();
            rowListResponse.Roles = new List<RoleRow>();
            rowListResponse.Roles = await result.ToListAsync();
            rowListResponse.LstCount = rowListResponse.Roles.Count();
            rowListResponse.ToSuccess<RoleListResponse>();
            return rowListResponse;
        }
        public async Task<UserListResponse> getUsersAsync(ListDTO listDTO)
        {
            try
            {


                UserListResponse userListResponse = new UserListResponse();
                userListResponse.Users = new List<UserRow>();
                var lst = Context.Users.Select(a => new UserRow { Id = a.Id, UserName = a.UserName, Email = a.Email }).AsQueryable();
                if (listDTO.Filter != null && listDTO.Filter != string.Empty)
                    lst = new LinqSearch().ApplyFilter(lst, listDTO.Filter);
                userListResponse.LstCount = await lst.CountAsync();
                userListResponse.Users = await lst.Skip((listDTO.PageNum - 1) * listDTO.PageSize).Take(listDTO.PageSize).ToListAsync();
                userListResponse.ToSuccess<UserListResponse>();
                return userListResponse;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<ClaimsResponse> getUserClaimsByUserIdAsync(string UserId)
        {
            try
            {
                ClaimsResponse claimsResponse = new ClaimsResponse();
                claimsResponse.Claims = new List<ClaimsRow>();

                var lst =  Context.UserRoles.Where(a => a.UserId == UserId).Select(a=>a.RoleId);
                var claimsIds =await Context.RoleClaims.Where(a => lst.Contains(a.RoleId)).Select(a => int.Parse(a.ClaimValue)).ToListAsync();
                var _claims =await Context.Claims.Where(a => claimsIds.Contains(a.Id)).Select(a=>a).ToListAsync();               
                foreach (var item in _claims)
                {
                    ClaimsRow claimsRow = new ClaimsRow();
                    item.CopyPropertiesTo(claimsRow);
                    claimsResponse.Claims.Add(claimsRow);
                }
                claimsResponse.LstCount = claimsResponse.Claims.Count();
                claimsResponse.ToSuccess<ClaimsResponse>();
                return claimsResponse;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
