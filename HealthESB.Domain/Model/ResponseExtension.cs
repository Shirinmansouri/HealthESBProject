using HealthESB.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace HealthESB.Domain.Model
{
    public static class ResponseExtension
    {
        public static T ToDuplicateUcid<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.DuplicateUcid;
            response.ErrorMessage = HealthESBApiResponseMessages.DuplicateUcid;
            response.HasError = true;
            return response;
        }
        public static T ToNotFoundPrescription<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.PrescriptionNotFound;
            response.ErrorMessage = HealthESBApiResponseMessages.PrescriptionNotFound;
            response.HasError = true;
            return response;
        }
        public static T ToSuccess<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.Success;
            response.ErrorMessage = HealthESBApiResponseMessages.Sucsses;
            response.HasError = false;
            return response;
        }
        public static T ToFailedAuthentication<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.AuthenticationError;
            response.ErrorMessage = HealthESBApiResponseMessages.AuthenticationError;
            response.HasError = true;
            return response;
        }
        public static T ToNotValidRequest<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.ValidationError;
            response.ErrorMessage = HealthESBApiResponseMessages.ValidationError;
            response.HasError = true;
            return response;
        }

        public static T ToApiError<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.ApiError;
            response.ErrorMessage = HealthESBApiResponseMessages.ApiError;
            response.HasError = true;
            return response;
        }
        public static T ToDuplicateCode<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.DuplicateCode;
            response.ErrorMessage = HealthESBApiResponseMessages.DuplicateCodeError;
            response.HasError = true;
            return response;
        }
        
        public static T ToCanNotChange<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.CanNotChangeError;
            response.ErrorMessage = HealthESBApiResponseMessages.CanNotChangeError;
            response.HasError = true;
            return response;
        }
        public static T ToRowNotFound<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.RowNotFound;
            response.ErrorMessage = HealthESBApiResponseMessages.RowNotFoundError;
            response.HasError = true;
            return response;
        }
        public static T ToDuplicateSerialNo<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.DuplicatePrescriptionError;
            response.ErrorMessage = HealthESBApiResponseMessages.DuplicatePrescriptionError;
            response.HasError = true;
            return response;
        }
        public static T ToIncompleteInput<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.IncompleteInput;
            response.ErrorMessage = HealthESBApiResponseMessages.IncompleteInput;
            response.HasError = true;
            return response;
        }

        public static T ToInvalidLenghtBarcodeUcid<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.InvalidLenghtBarcodeUcid;
            response.ErrorMessage = HealthESBApiResponseMessages.InvalidLenghtBarcodeUcid;
            response.HasError = true;
            return response;
        }
        public static T ToIsSuccessUcid<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.IsSuccessConfirmUcid;
            response.ErrorMessage = HealthESBApiResponseMessages.IsSuccessConfirmUcid;
            response.HasError = false;
            return response;
        }
        public static T ToDuplicateRole<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.DuplicateRole;
            response.ErrorMessage = HealthESBApiResponseMessages.DuplicateRole;
            response.HasError = false;
            return response;
        }
        public static T ToDulicateUserName<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.DulicateUserName;
            response.ErrorMessage = HealthESBApiResponseMessages.DulicateUserName;
            response.HasError = false;
            return response;
        }
        public static T ToInvalidUserNameOrPassword<T>(this T response) where T : BaseResponse
        {
            response.ErrorCode = (int)HealthESBApiResponseCode.InvalidUserNameOrPassword;
            response.ErrorMessage = HealthESBApiResponseMessages.InvalidUserNameOrPassword;
            response.HasError = false;
            return response;
        }

 
    }
}
