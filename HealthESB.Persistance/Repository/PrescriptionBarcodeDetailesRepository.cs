using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.Model;
using HealthESB.EF;
using HealthESB.EF.DynamicFilter;
using HealthESB.Framework.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Persistance.Repository
{
    public class PrescriptionBarcodeDetailesRepository : GenericRepository<PrescriptionBarcodeDetailes, HealthESBDbContext>,
           IPrescriptionBarcodeDetailesRepository
    {
        public PrescriptionBarcodeDetailesRepository(HealthESBDbContext context) : base(context)
        {
        }

        public async Task<PrescriptionActivityListResponse> GetPrescriptionActivityListByPaging(ListDTO listDTO)
        {
            PrescriptionActivityListResponse prescriptionActivityListResponse = new PrescriptionActivityListResponse();
            var lst = (from a in Context.Set<PrescriptionBarcodeDetailes>()
                       join b in Context.Set<PrescriptionBarcode>()
                       on a.PrescriptionBarcodeId equals b.Id
                       join p in Context.Set<Prescription>()
                       on b.PrescriptionId equals p.Id
                       join s in Context.Set<PrescriptionBarcodeDetailesType>()
                       on a.PrescriptionBarcodeDetailesTypesId equals s.Id
                       join y in Context.Set<PrescriptionBarcodeStatus>()
                       on b.PrescriptionBarcodeStatusId equals y.Id
                       select new PrescriptionActivityRow
                       {
                           Status = a.Status,
                           StatusMessage = a.StatusMessage,
                           BatchCode = a.BatchCode,
                           EnglishName = a.EnglishName,
                           Expiration = a.Expiration,
                           Irc = a.Irc,
                           PersianName = a.PersianName,
                           Price = a.Price,
                           ProductType = a.ProductType,
                           TrackingCode = a.TrackingCode,
                           UnitConsumed = a.UnitConsumed,
                           Manufacturing = a.Manufacturing,
                           GenericCode = a.GenericCode,
                           ProductTypeId = a.ProductTypeId,
                           BarcodeUid = a.BarcodeUid,
                           PrescriptionBarcodeId = a.PrescriptionBarcodeId,
                           PrescriptionId = b.PrescriptionId,
                           CreatedDate = a.CreatedDate,
                           UpdatedDate = a.UpdatedDate,
                           PrescriptionBarcodeDetailesTypesId = a.PrescriptionBarcodeDetailesTypesId,
                           PrescriptionBarcodeStatusId = b.PrescriptionBarcodeStatusId,
                           PrescriptionBarcodeDetailesTypesName = s.Name,
                           PrescriptionBarcodeStatusName = y.Name,
                           PatientGivenName = p.PatientGivenName,
                           PatientSurname = p.PatientSurname,
                           PatientNationalCode = p.PatientNationalCode,
                           PhysicianGivenName = p.PhysicianGivenName,
                           PhysicianSurname = p.PhysicianSurName,
                           MedicalCouncilNumber = p.MedicalCouncilNumber,
                           PharmacyGln = p.PharmacyGln,
                           BasicInsurance = p.BasicInsurance,
                           ComplementaryInsurance = p.ComplementaryInsurance,
                           Uid = a.Uid,
                           Amount = b.Amount,
                           OutPrescriptionId=p.OutPrescriptionId
                       }).AsQueryable();
            if (listDTO.Filter != null && listDTO.Filter != string.Empty)
                lst = new LinqSearch().ApplyFilter(lst, listDTO.Filter);

            prescriptionActivityListResponse.LstCount = await lst.CountAsync();
            prescriptionActivityListResponse.LstPrescriptionActivityRow = await lst.Skip((listDTO.PageNum - 1) * listDTO.PageSize).Take(listDTO.PageSize).ToListAsync();
            return prescriptionActivityListResponse;

        }

    }
}
