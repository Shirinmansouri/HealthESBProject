using HealthESB.Domain.IService;
using HealthESB.Domain.Model.Avihang;
using HealthESB.Framework.Utility;
using HealthESB.Infrastructure.Channel;
using HealthESB.Infrastructure.Model.Channel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace HealthESB.API.Controllers
{
    public class TestController : Controller
    {

        private readonly IAvihangUserSessionsService _avihangUserSessionsService;
        public TestController(IAvihangUserSessionsService avihangUserSessionsService)
        {

            _avihangUserSessionsService = avihangUserSessionsService;


        }
        [HttpPost("Create")]
        public async void Create()
        {
            var  baseResponse = await _avihangUserSessionsService.GetUserSession(318054, 318269, 151971);
  
        }
    }
}
