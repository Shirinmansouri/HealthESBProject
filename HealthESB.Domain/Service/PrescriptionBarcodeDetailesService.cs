using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using HealthESB.Domain.Model;
using HealthESB.Framework.Logger;
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

    }
}
