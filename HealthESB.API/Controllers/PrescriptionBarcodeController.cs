using HealthESB.Domain.IService;
using HealthESB.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthESB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionBarcodeController : ControllerBase
    {
        private readonly IPrescriptionBarcodeService _prescriptionBarcodeService;
        public PrescriptionBarcodeController(IPrescriptionBarcodeService prescriptionBarcodeService)
        {
            _prescriptionBarcodeService = prescriptionBarcodeService;
        }

        [HttpPost("Create")]
       // [Authorize]
        public async Task<PrescriptionBarcodeResponse> Create([FromBody] PrescriptionBarcodeRequest prescriptionBarcodeRequest)
        {

            return await _prescriptionBarcodeService.Create(prescriptionBarcodeRequest);

        }
        [HttpPost("ReActiveUid")]
        //[Authorize]
        public async Task<ReactiveResponse> ReActiveUid([FromBody] ReactiveRequest reactiveRequest)
        {

            return await _prescriptionBarcodeService.ReActive(reactiveRequest);

        }
        [HttpPost("ReActiveByPrescriptionId")]
       //[Authorize]
        public async Task<ReactiveResponse> ReActiveByPrescriptionId([FromBody] GroupReactiveRequest groupReactiveRequest)
        {
            return await _prescriptionBarcodeService.ReActivePrescriptionId(groupReactiveRequest.PrescriptionId);

        }
        [HttpPost("Confirm")]
       // [Authorize]
        public async Task<ConfirmResponse> confirm([FromBody] ConfirmRequest confirmRequest)
        {

            return await _prescriptionBarcodeService.ConfirmUid(confirmRequest);

        }
    }
}
