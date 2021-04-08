using HealthESB.Domain.IService;
using HealthESB.Domain.Model;
using HealthESB.EF.DynamicFilter;
using HealthESB.Framework.Utility;
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
    [UserAccess]
    public class PrescriptionBarcodeDetailesController : ControllerBase
    {
        private readonly IPrescriptionBarcodeDetailesService _prescriptionBarcodeDetailesService;
        public PrescriptionBarcodeDetailesController(IPrescriptionBarcodeDetailesService prescriptionBarcodeDetailesService)
        {
            _prescriptionBarcodeDetailesService = prescriptionBarcodeDetailesService;
        }
        [HttpPost("GetPrescriptionActivity")]
        [Authorize]
        public async Task<PrescriptionActivityListResponse> GetPrescriptionActivity([FromBody] ListDTO listDTO)
        {
         

            //SearchFilter searchFilter = new SearchFilter();
            //searchFilter.groupOp = new FilterEnum.GroupOpEnum();
            //searchFilter.groupOp = FilterEnum.GroupOpEnum.And;
            //searchFilter.rules = new List<SearchRule>();
            //searchFilter.rules.Add(new SearchRule() { field = "PatientNationalCode", data = "2872257901", op = FilterEnum.OpEnum.cn });
            //searchFilter.rules.Add(new SearchRule() { field = "PharmacyGln", data = "6261007574879", op = FilterEnum.OpEnum.cn });
            //searchFilter.rules.Add(new SearchRule() { field = "MedicalCouncilNumber", data = "123", op = FilterEnum.OpEnum.cn });
            //searchFilter.rules.Add(new SearchRule() { field = "Uid", data = "11101010040346508628", op = FilterEnum.OpEnum.cn });
            //searchFilter.rules.Add(new SearchRule() { field = "CreatedDate", data =DateTime.Now.AddDays(-20).ToString(), op = FilterEnum.OpEnum.ge});
            //searchFilter.rules.Add(new SearchRule() { field = "TrackingCode", data = "2854", op = FilterEnum.OpEnum.cn });
            //listDTO.Filter = Newtonsoft.Json.JsonConvert.SerializeObject(searchFilter);
            //listDTO.IsRequestCount = false;
            return await _prescriptionBarcodeDetailesService.GetPrescriptionActivityList(listDTO);

        }
        [HttpPost("GetPrescriptionBarcodeForActivation")]
       [Authorize]
        public async Task<PrescriptionActivityListResponse> GetPrescriptionBarcodeForActivation([FromBody] GroupReactiveRequest groupReactiveRequest)
        {

            ListDTO listDTO = new ListDTO();
            SearchFilter searchFilter = new SearchFilter();
            searchFilter.groupOp = new FilterEnum.GroupOpEnum();
            searchFilter.groupOp = FilterEnum.GroupOpEnum.And;
            searchFilter.rules = new List<SearchRule>();
            searchFilter.rules.Add(new SearchRule() { field = "PrescriptionId", data = groupReactiveRequest.PrescriptionId.ToString(), op = FilterEnum.OpEnum.eq });
            searchFilter.rules.Add(new SearchRule() { field = "Status", data = "0", op = FilterEnum.OpEnum.eq });
            listDTO.Filter = Newtonsoft.Json.JsonConvert.SerializeObject(searchFilter);
            listDTO.IsRequestCount = false;
            listDTO.PageNum = 1;
            listDTO.PageSize = 1000;
            return await _prescriptionBarcodeDetailesService.GetPrescriptionActivityList(listDTO);

        }
    }
}
