using HealthESB.Domain.IService;
using HealthESB.Domain.Model;
using HealthESB.Framework.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HealthESB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<PrescriptionResponse> Create([FromBody] PrescriptionRequest prescriptionRequest)
        {

            return await _prescriptionService.Create(prescriptionRequest);

        }

    }
}
