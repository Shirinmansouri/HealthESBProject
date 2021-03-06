using HealthESB.Domain.Entities;
using HealthESB.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface IPrescriptionService
    {
        Task<PrescriptionResponse> Create(PrescriptionRequest prescriptionRequest);
        Task<Prescription> GetPrescriptionByOutPrescriptionId(long OutPrescriptionId);
    }
}
