using HealthESB.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface IPrescriptionBarcodeService
    {
        Task<PrescriptionBarcodeResponse> Create(PrescriptionBarcodeRequest prescriptionBarcodeRequest);
        Task<ReactiveResponse> ReActive(ReactiveRequest reactiveRequest);
        Task<ConfirmResponse> ConfirmUid(ConfirmRequest confirmRequest);
        Task<ReactiveResponse> ReActivePrescriptionId(long PrescriptionId);
    }
}
