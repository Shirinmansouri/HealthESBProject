using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using HealthESB.Domain.Model;
using HealthESB.Framework.Logger;
using HealthESB.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Service
{
    public class PrescriptionBarcodeDetailesService : IPrescriptionBarcodeDetailesService
    {

        private IPrescriptionBarcodeDetailesRepository _prescriptionBarcodeDetailesRepository;
        private ILogService _logService;
        public PrescriptionBarcodeDetailesService(IPrescriptionBarcodeDetailesRepository prescriptionBarcodeDetailesRepository, ILogService logService)
        {
            _prescriptionBarcodeDetailesRepository = prescriptionBarcodeDetailesRepository;
            _logService = logService;


        }
        public async Task<PrescriptionActivityListResponse> GetPrescriptionActivityList(ListDTO listDTO)
        {
            var response = new PrescriptionActivityListResponse();
            try
            {
                response = await _prescriptionBarcodeDetailesRepository.GetPrescriptionActivityListByPaging(listDTO);
                return response.ToSuccess<PrescriptionActivityListResponse>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<ReactiveResponse> GetPrescriptionBarcodeDetailesByPrescriptionId(long prescriptionId)
        {
            var response = new ReactiveResponse();

            try
            {
                var result = await _prescriptionBarcodeDetailesRepository.GetWhere(a => a.PrescriptionBarcode.PrescriptionId == prescriptionId);
                response.ItemsInfo = new List<PrescriptionBarcodeDetailesResponse>();
                PrescriptionBarcodeDetailesResponse prescriptionBarcodeDetailesResponse;
                foreach (var item in result)
                {
                    prescriptionBarcodeDetailesResponse = new PrescriptionBarcodeDetailesResponse();
                    result.CopyPropertiesTo(prescriptionBarcodeDetailesResponse);
                    response.ItemsInfo.Add(prescriptionBarcodeDetailesResponse);
                }
                return response.ToSuccess<ReactiveResponse>();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
