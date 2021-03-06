﻿using HealthESB.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthESB.Framework.Logger;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.Model;
using HealthESB.Domain.Entities;
using HealthESB.Framework.Utility;
using HealthESB.Infrastructure;
using HealthESB.Infrastructure.Model;

namespace HealthESB.Domain.Service
{
    public class PrescriptionService: IPrescriptionService

    {
        private ILogService _logService;
        private IPrescriptionRepository _PrescriptionRepository;
        public PrescriptionService(IPrescriptionRepository prescriptionRepository ,ILogService logService)
        {
            _logService = logService;
            _PrescriptionRepository = prescriptionRepository;
        }

        public async Task<PrescriptionResponse> Create(PrescriptionRequest prescriptionRequest)
        {
            var response = new PrescriptionResponse();
            try
            {
                TTAC tTAC = new TTAC();
                Prescription prescription = new Prescription();
                prescriptionRequest.CopyPropertiesTo(prescription);
                await _PrescriptionRepository.Add(prescription);
                TTACPrescriptionRequest tTACPrescriptionRequest = new TTACPrescriptionRequest();
                TTACPrescriptionResponse tTACPrescriptionResponse = new TTACPrescriptionResponse();
                prescriptionRequest.CopyPropertiesTo(tTACPrescriptionRequest);
                tTACPrescriptionRequest.Username = Constants.TTAC_UserName;
                tTACPrescriptionRequest.Password = Constants.TTAC_Password;
                tTACPrescriptionResponse =await tTAC.CallPrescriptionApi(tTACPrescriptionRequest);
                response.ErrorCode =tTACPrescriptionResponse.ErrorCode;
                response.ErrorMessage = tTACPrescriptionResponse.ErrorMessage;
                response.PrescriptionId = tTACPrescriptionResponse.PrescriptionId;
                prescription.OutErrorCode = tTACPrescriptionResponse.ErrorCode;
                prescription.OutErrorMessage = tTACPrescriptionResponse.ErrorMessage;
                prescription.OutPrescriptionId = tTACPrescriptionResponse.PrescriptionId;
                await _PrescriptionRepository.Update(prescription);
                return response;

            }
            catch (Exception e)
            {
                _logService.LogText("PrescriptionInternalError"+e.Message.ToString());
                throw new Exception(e.Message);
            }
        }

        public  async Task<Prescription> GetPrescriptionByOutPrescriptionId(long OutPrescriptionId)
        {
           return await  _PrescriptionRepository.FirstOrDefault(a => a.OutPrescriptionId == OutPrescriptionId);
        }
    }
}