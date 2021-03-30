using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.Model;
using HealthESB.EF;
using HealthESB.EF.DynamicFilter;
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

                var lst =  Context.UserRoles.Where(a => a.UserId == UserId).AsQueryable();
                var result =  Context.Roles.Join(lst, a => a.Id, b => b.RoleId, (a, b) => new RoleRow { Name = a.Name, Id = a.Id }).AsQueryable();
                var claimsIds =   Context.RoleClaims.Join(result, a => a.RoleId, b => b.Id, (a, b) => new ClaimsRow { Id = int.Parse(a.ClaimValue) }).AsQueryable();
                var Claims = (from a in Context.Set<Claims>()
                              join b in claimsIds on a.Id equals b.Id
                              select new ClaimsRow
                              {
                                  ActionName = a.ActionName,
                                  ActionTitleEn = a.ActionTitleEn,
                                  ActionTitleFr = a.ActionTitleFr,
                                  ControlleEnTitile = a.ControlleEnTitile,
                                  ControlleFaTitile = a.ControlleFaTitile,
                                  ControllerEntityID = a.ControllerEntityID,
                                  ControllerName = a.ControllerName,
                                  Id = a.Id
                              }).AsQueryable();
                ClaimsResponse claimsResponse = new ClaimsResponse();
                claimsResponse.Claims = new List<ClaimsRow>();
                claimsResponse.Claims = await Claims.ToListAsync();
                claimsResponse.LstCount = claimsResponse.Claims.Count();
                return claimsResponse;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
