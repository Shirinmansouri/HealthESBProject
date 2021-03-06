using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthESB.Domain.Entities;
using HealthESB.Domain.Model;

namespace HealthESB.Domain.IRepository
{
    public interface IPrescriptionBarcodeDetailesRepository :IGenericRepository<PrescriptionBarcodeDetailes>
    {
        Task<PrescriptionActivityListResponse> GetPrescriptionActivityListByPaging(ListDTO listDTO);
    }
}
