using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Framework.Utility
{

    public static class Constants
    {
        public static string TTAC_BaseUrl;
        public static string TTAC_UserName;
        public static string TTAC_Password;
        public static string TTAC_RegisterApiName;
        public static string TTAC_RegisterApiKey;
        public static string TTAC_CheckApiName;
        public static string TTAC_CheckApiKey;
        public static string TTAC_ReactiveApiName;
        public static string TTAC_ReactiveApiKey;
        public static string LoginUserName;
        public static int TokenExpirationHours;
        public static string TokenKey;
        public static string  LoginUserId;
      
    }
    public enum HealthESBApiResponseCode
    {
        Success = 0,
        ApiError = -62,
        ValidationError = 2,
        AuthenticationError = 3,
        UserNotFound = 4,
        DuplicateCode = 5,
        CanNotChangeError = 6,
        RowNotFound = 7,
        DuplicatePrescriptionError=8,
        PrescriptionNotFound=9,
        DuplicateUcid=10,
        IncompleteInput=-91,
        ServiceProviderNotResponse=500,
        InvalidLenghtBarcodeUcid = 56,
        IsSuccessConfirmUcid=1,
        DuplicateRole=11,
        DulicateUserName=12,
        InvalidUserNameOrPassword=13
        
    }
   
    public enum PrescriptionBarcodeStatusEnum
    {
        InsertRequest = 1,
        GetResponseFromServiceProvider = 2,
        NotResponseServiceProvider = 3,
        FinalConfirmRequest= 4,
        ReActiveRequest = 5 
    }
    public enum PrescriptionBarcodeDetailesTypeEnum
    {
        RegisterBarcodeItems = 1,
        ReActiveBarcodeItems= 2 
    }
    public static class HealthESBApiResponseMessages
    {
        public static string Sucsses { get { return "عملیات با موفقیت انجام شد"; } }
        public static string ValidationError { get { return "خطا در اعتبار سنجی"; } }
        public static string AuthenticationError { get { return "مشکل در احراز هویت کاربر"; } }
        public static string UserNotFound { get { return "کاربر پیدا نشد"; } }
        public static string ApiError { get { return "خطا در فراخوانی سرویس"; } }
        public static string DuplicateCodeError { get { return "کد تکراری"; } }
        public static string CanNotChangeError { get { return "قابل تغییر نیست"; } }
        public static string RowNotFoundError { get { return "رکوردی با مشخصات ارسالی یافت نشد"; } }
        public static string DuplicatePrescriptionError { get { return "شماره نسخه تکراری"; } }
        public static string PrescriptionNotFound { get { return "نسخه ای با این کد قبلا ثبت نشده است "; } }
        public static string DuplicateUcid { get { return " این بارکد قبلا برای همین نسخه ثبت شده است"; } }
        public static string IncompleteInput { get { return " اطلاعات ارسالی ناقص میباشد"; } }
        public static string ServiceProviderNotResponse { get { return " عدم پاسخ دهی سرویس دهنده"; } }
        public static string InvalidLenghtBarcodeUcid { get { return " کد بارکد ثبت شده برای دارو دارای طول نامعتبر می باشد"; } }
        public static string IsSuccessConfirmUcid { get { return " عملیات با موفقیت انجام شد"; } }
        public static string DuplicateRole { get { return " این رل قبلا تعریف شده است"; } }
        public static string DulicateUserName { get { return " نام کاربری تکراری می باشد"; } }
        public static string InvalidUserNameOrPassword { get { return "نام کاربری یا کلمه عبور اشتباه می باشد"; } }

    }
}
