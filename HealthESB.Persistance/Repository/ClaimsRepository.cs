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
    public class ClaimsRepository : GenericRepository<Claims, HealthESBDbContext>,
            IClaimsRepository
    {
        public ClaimsRepository(HealthESBDbContext context) : base(context)
        {
        }

        public async Task<ClaimsResponse> GetClaimListByPaging(ListDTO listDTO)
        {
            ClaimsResponse claimsResponse = new ClaimsResponse();
            var lst = (from a in Context.Set<Claims>()
                       select new ClaimsRow() { Id = a.Id, ActionName = a.ActionName, ActionTitleEn = a.ActionTitleEn,
                       ActionTitleFr=a.ActionTitleFr,ControlleEnTitile=a.ControlleEnTitile,ControlleFaTitile=a.ControlleFaTitile,
                       ControllerEntityID=a.ControllerEntityID,ControllerName=a.ControllerName}).AsQueryable();
            if (listDTO.Filter != null && listDTO.Filter.Trim().Length != 0)
                lst = new LinqSearch().ApplyFilter(lst, listDTO.Filter);
            if (listDTO.IsRequestCount)
                claimsResponse.LstCount = await lst.CountAsync();
            else
                claimsResponse.Claims = await lst.Skip((listDTO.PageNum - 1) * listDTO.PageSize).Take(listDTO.PageSize).ToListAsync();
            return claimsResponse;
        }
    }
}


