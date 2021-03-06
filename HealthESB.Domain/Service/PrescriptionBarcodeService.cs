using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthESB.Domain.IService;
using HealthESB.Domain.IRepository;
using HealthESB.Framework.Logger;
using HealthESB.Domain.Model;
using HealthESB.Infrastructure;
using HealthESB.Domain.Entities;
using HealthESB.Framework.Utility;
using HealthESB.Infrastructure.Model;

namespace HealthESB.Domain.Service
{
    public class PrescriptionBarcodeService : IPrescriptionBarcodeService
    {
        private IPrescriptionRepository _prescriptionRepository;
        private IPrescriptionBarcodeRepository _prescriptionBarcodeRepository;
        private IPrescriptionBarcodeDetailesRepository _prescriptionBarcodeDetailesRepository;
        private ILogService _logService;
        public PrescriptionBarcodeService(IPrescriptionRepository prescriptionRepository, IPrescriptionBarcodeRepository prescriptionBarcodeRepository, ILogService logService, IPrescriptionBarcodeDetailesRepository prescriptionBarcodeDetailesRepository)
        {
            _prescriptionBarcodeRepository = prescriptionBarcodeRepository;
            _logService = logService;
            _prescriptionBarcodeDetailesRepository = prescriptionBarcodeDetailesRepository;
            _prescriptionRepository = prescriptionRepository;

        }

        public async Task<PrescriptionBarcodeResponse> Create(PrescriptionBarcodeRequest prescriptionBarcodeRequest)
        {
            var response = new PrescriptionBarcodeResponse();
            response.ItemsInfo = new List<PrescriptionBarcodeDetailesResponse>();
            var prescriptionBarcodeDetailes = new PrescriptionBarcodeDetailes();
            var prescriptionBarcodeDetailesResponse = new PrescriptionBarcodeDetailesResponse();
            try
            {

                if (string.IsNullOrEmpty(prescriptionBarcodeRequest.BarcodeUid) || string.IsNullOrEmpty(prescriptionBarcodeRequest.GenericCode) || prescriptionBarcodeRequest.Amount == 0 || prescriptionBarcodeRequest.PrescriptionId == 0)
                {
                    return response.ToIncompleteInput<PrescriptionBarcodeResponse>();
                }

                if (string.IsNullOrEmpty(prescriptionBarcodeRequest.ReCheckCode)) prescriptionBarcodeRequest.ReCheckCode = Guid.NewGuid().ToString();

                TTAC tTAC = new TTAC();
                PrescriptionBarcode prescriptionBarcode = new PrescriptionBarcode();
                prescriptionBarcodeRequest.CopyPropertiesTo(prescriptionBarcode);
                var result = await _prescriptionRepository.FirstOrDefault(a => a.OutPrescriptionId == prescriptionBarcodeRequest.PrescriptionId);
                if (result == null)
                    return response.ToNotFoundPrescription<PrescriptionBarcodeResponse>();


                prescriptionBarcode.PrescriptionId = result.Id;
                prescriptionBarcode.PrescriptionBarcodeStatusId = (int)PrescriptionBarcodeStatusEnum.InsertRequest;
                await _prescriptionBarcodeRepository.Add(prescriptionBarcode);

                if (prescriptionBarcodeRequest.BarcodeUid.Length > 80)
                {
                    prescriptionBarcode.OutErrorCode = (int)HealthESBApiResponseCode.InvalidLenghtBarcodeUcid;
                    prescriptionBarcode.OutErrorMessage = HealthESBApiResponseMessages.InvalidLenghtBarcodeUcid;
                    await _prescriptionBarcodeRepository.Update(prescriptionBarcode);
                    return response.ToInvalidLenghtBarcodeUcid<PrescriptionBarcodeResponse>();
                }

                TTACPrescriptionBarcodeRequest tTACPrescriptionBarcodeRequest = new TTACPrescriptionBarcodeRequest();
                TTACPrescriptionBarcodeResponse tTACPrescriptionBarcodeResponse = new TTACPrescriptionBarcodeResponse();
                prescriptionBarcodeRequest.CopyPropertiesTo(tTACPrescriptionBarcodeRequest);


                tTACPrescriptionBarcodeRequest.Username = Constants.TTAC_UserName;
                tTACPrescriptionBarcodeRequest.Password = Constants.TTAC_Password;


                tTACPrescriptionBarcodeResponse = await tTAC.CallCheckSingleBarcodeApi(tTACPrescriptionBarcodeRequest);
                prescriptionBarcode.OutErrorCode = tTACPrescriptionBarcodeResponse.ErrorCode;
                prescriptionBarcode.OutErrorMessage = tTACPrescriptionBarcodeResponse.ErrorMessage;
                response.ErrorCode = tTACPrescriptionBarcodeResponse.ErrorCode;
                response.ErrorMessage = tTACPrescriptionBarcodeResponse.ErrorMessage;
                response.PrescriptionId = tTACPrescriptionBarcodeResponse.PrescriptionId;
                prescriptionBarcode.PrescriptionBarcodeStatusId = (int)PrescriptionBarcodeStatusEnum.GetResponseFromServiceProvider;
                prescriptionBarcode.ReCheckCode = prescriptionBarcodeRequest.ReCheckCode;
                await _prescriptionBarcodeRepository.Update(prescriptionBarcode);

                foreach (var item in tTACPrescriptionBarcodeResponse.ItemsInfo)
                {
                    prescriptionBarcodeDetailes = new PrescriptionBarcodeDetailes();
                    item.CopyPropertiesTo(prescriptionBarcodeDetailes);
                    item.CopyPropertiesTo(prescriptionBarcodeDetailesResponse);
                    response.ItemsInfo.Add(prescriptionBarcodeDetailesResponse);
                    prescriptionBarcodeDetailes.PrescriptionBarcodeId = prescriptionBarcode.Id;
                    prescriptionBarcodeDetailes.PrescriptionBarcodeDetailesTypesId = (int)PrescriptionBarcodeDetailesTypeEnum.RegisterBarcodeItems;
                    await _prescriptionBarcodeDetailesRepository.Add(prescriptionBarcodeDetailes);

                }


                return response;

            }
            catch (Exception e)
            {
                _logService.LogText("PrescriptionBarcodeInternalError" + e.Message.ToString());
                throw new Exception(e.Message);
            }
        }
        public async Task<ReactiveResponse> ReActive(ReactiveRequest reactiveRequest)
        {
            var response = new ReactiveResponse();
            response.ItemsInfo = new List<PrescriptionBarcodeDetailesResponse>();
            var prescriptionBarcodeDetailes = new PrescriptionBarcodeDetailes();
            var prescriptionBarcodeDetailesResponse = new PrescriptionBarcodeDetailesResponse();
            try
            {
                if (string.IsNullOrEmpty(reactiveRequest.BarcodeUid) || string.IsNullOrEmpty(reactiveRequest.PharmacyGln) || reactiveRequest.Amount == 0 || reactiveRequest.TrackingCode == 0)
                    return response.ToIncompleteInput<ReactiveResponse>();

                TTAC tTAC = new TTAC();

                var result = await _prescriptionBarcodeDetailesRepository.FirstOrDefault(a => a.TrackingCode == reactiveRequest.TrackingCode && a.BarcodeUid == reactiveRequest.BarcodeUid);
                if (result == null)
                    return response.ToRowNotFound<ReactiveResponse>();
                prescriptionBarcodeDetailes.PrescriptionBarcodeId = result.PrescriptionBarcodeId;
                prescriptionBarcodeDetailes.PrescriptionBarcodeDetailesTypesId = (int)PrescriptionBarcodeDetailesTypeEnum.ReActiveBarcodeItems;

                TTACReactiveRequest tTACReactiveRequest = new TTACReactiveRequest();
                TTACReactiveResponse tTACReactiveResponse = new TTACReactiveResponse();

                reactiveRequest.CopyPropertiesTo(tTACReactiveRequest);
                tTACReactiveRequest.Username = Constants.TTAC_UserName;
                tTACReactiveRequest.Password = Constants.TTAC_Password;
                tTACReactiveResponse = await tTAC.CallReactiveApi(tTACReactiveRequest);
                response.ErrorCode = tTACReactiveResponse.ErrorCode;
                response.ErrorMessage = tTACReactiveResponse.ErrorMessage;

                foreach (var item in tTACReactiveResponse.ItemsInfo)
                {
                    prescriptionBarcodeDetailes = new PrescriptionBarcodeDetailes();
                    item.CopyPropertiesTo(prescriptionBarcodeDetailes);
                    item.CopyPropertiesTo(prescriptionBarcodeDetailesResponse);
                    response.ItemsInfo.Add(prescriptionBarcodeDetailesResponse);
                    prescriptionBarcodeDetailes.PrescriptionBarcodeId = result.PrescriptionBarcodeId;
                    prescriptionBarcodeDetailes.PrescriptionBarcodeDetailesTypesId = (int)PrescriptionBarcodeDetailesTypeEnum.ReActiveBarcodeItems;
                    if (tTACReactiveResponse.ErrorCode == 0 && item.Status == 100)
                    {
                        var prescriptionBarcode = await _prescriptionBarcodeRepository.FirstOrDefault(a => a.Id == result.PrescriptionBarcodeId);
                        prescriptionBarcode.PrescriptionBarcodeStatusId = (int)PrescriptionBarcodeStatusEnum.ReActiveRequest;
                        await _prescriptionBarcodeRepository.Update(prescriptionBarcode);
                    }
                    await _prescriptionBarcodeDetailesRepository.Add(prescriptionBarcodeDetailes);

                }

                return response;
            }
            catch (Exception e)
            {
                _logService.LogText("PrescriptionBarcodeInternalError" + e.Message.ToString());
                throw new Exception(e.Message);
            }
        }
        public async Task<ConfirmResponse> ConfirmUid(ConfirmRequest confirmRequest)
        {
            ConfirmResponse response = new ConfirmResponse();
            try
            {
                foreach (var item in confirmRequest.Uids)
                {
                    var result =await _prescriptionBarcodeRepository.FirstOrDefault(a => a.ReCheckCode == item.ReCheckCode);
                    if (result != null)
                    {
                        result.PrescriptionBarcodeStatusId = (int)PrescriptionBarcodeStatusEnum.FinalConfirmRequest;
                        await _prescriptionBarcodeRepository.Update(result);
                    }

                }
                response.ToIsSuccessUcid<ConfirmResponse>();
                response.resCode = response.ErrorCode;
                response.resMessage = response.resMessage;
                return response;
            }
            catch (Exception ex )
            {
                response.ToApiError<ConfirmResponse>();
                response.resCode = response.ErrorCode;
                response.resMessage = response.resMessage;
                return response;
            }
        }
    }

}
