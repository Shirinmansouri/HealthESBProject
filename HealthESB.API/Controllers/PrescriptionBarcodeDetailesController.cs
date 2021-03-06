using HealthESB.Domain.IService;
using HealthESB.Domain.Model;
using HealthESB.EF.DynamicFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HealthESB.API.Controllers
{
    public class PrescriptionBarcodeDetailesController : ControllerBase
    {
        private readonly IPrescriptionBarcodeDetailesService _prescriptionBarcodeDetailesService;
        public PrescriptionBarcodeDetailesController(IPrescriptionBarcodeDetailesService prescriptionBarcodeDetailesService)
        {
            _prescriptionBarcodeDetailesService = prescriptionBarcodeDetailesService;
        }
        [HttpPost("GetPrescriptionActivityList")]
        // [Authorize]
        public async Task<PrescriptionActivityListResponse> GetPrescriptionActivityList([FromBody] ListDTO listDTO)
        {
            //SearchFilter searchFilter = new SearchFilter();
            //searchFilter.rules = new List<SearchRule>();
            //searchFilter.rules.Add(new SearchRule() { field = "Status", data = "100", op = FilterEnum.OpEnum.eq });
            //listDTO.Filter = Newtonsoft.Json.JsonConvert.SerializeObject(searchFilter);
            //listDTO.IsRequestCount = false;
            return await _prescriptionBarcodeDetailesService.GetPrescriptionActivityList(listDTO);

        }
    }
}
