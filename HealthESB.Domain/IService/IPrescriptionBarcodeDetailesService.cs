using HealthESB.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface IPrescriptionBarcodeDetailesService
    {
        Task<PrescriptionActivityListResponse> GetPrescriptionActivityList(ListDTO listDTO);
        Task<ReactiveResponse> GetPrescriptionBarcodeDetailesByPrescriptionId(long prescriptionId);
    }
}
