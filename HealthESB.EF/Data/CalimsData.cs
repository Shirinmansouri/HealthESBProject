using HealthESB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.EF.Data
{
    public static class CalimsData
    {
        public static int Seed(ModelBuilder modelBuilder, int id)
        {
            var costsActions = new List<Claims>();

            var costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "PrescriptionBarcode",
                ActionName = "Create",
                ControllerEntityID= 2,
                ControlleEnTitile= "PrescriptionBarcode",
                ControlleFaTitile="اقلام نسخه",
                ActionTitleEn="Create",
                ActionTitleFr="ایجاد"     
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "PrescriptionBarcode",
                ActionName = "ReActiveUid",
                ControllerEntityID = 2,
                ControlleEnTitile = "PrescriptionBarcode",
                ControlleFaTitile = "اقلام نسخه",
                ActionTitleEn = "ReActiveUid",
                ActionTitleFr = "فعال سازی مجدد"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "PrescriptionBarcode",
                ActionName = "confirm",
                ControllerEntityID = 2,
                ControlleEnTitile = "PrescriptionBarcode",
                ControlleFaTitile = "اقلام نسخه",
                ActionTitleEn = "confirm",
                ActionTitleFr = "تایید نهایی اقلام"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "Prescription",
                ActionName = "Create",
                ControllerEntityID = 1,
                ControlleEnTitile = "Prescription",
                ControlleFaTitile = "نسخه",
                ActionTitleEn = "Create",
                ActionTitleFr = "ایجاد نسخه"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "PrescriptionBarcode",
                ActionName = "ReActiveByPrescriptionId",
                ControllerEntityID = 2,
                ControlleEnTitile = "PrescriptionBarcode",
                ControlleFaTitile = "اقلام نسخه",
                ActionTitleEn = "ReActiveByPrescriptionId",
                ActionTitleFr = "فعال سازی گروهی با شماره نسخه"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "PrescriptionBarcodeDetailes",
                ActionName = "GetPrescriptionActivity",
                ControllerEntityID = 3,
                ControlleEnTitile = "PrescriptionBarcodeDetailes",
                ControlleFaTitile = "جزئیات اقلام نسخه",
                ActionTitleEn = "GetPrescriptionActivity",
                ActionTitleFr = "تاریخچه ی درخواست های ارسالی"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "PrescriptionBarcodeDetailes",
                ActionName = "GetPrescriptionBarcodeForActivation",
                ControllerEntityID = 3,
                ControlleEnTitile = "PrescriptionBarcodeDetailes",
                ControlleFaTitile = "جزئیات اقلام نسخه",
                ActionTitleEn = "GetPrescriptionBarcodeForActivation",
                ActionTitleFr = "جزئیات اقلام های نسخه های ارسالی"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "CreateUser",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "CreateUser",
                ActionTitleFr = "ایجاد کاربر"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "UpdateUser",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "UpdateUser",
                ActionTitleFr = "به روززسانی کاربر"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "CreateRoles",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "CreateRoles",
                ActionTitleFr = "ایجاد نقش"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "UpdateRoles",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "UpdateRoles",
                ActionTitleFr = "به روزرسانی نقش"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "GetRoles",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "GetRoles",
                ActionTitleFr = "لیست نقش ها"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "getUserRolesByUserIdAsync",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "getUserRolesByUserIdAsync",
                ActionTitleFr = "دریافت نقش های کاربر"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "getUsersAsync",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "getUsersAsync",
                ActionTitleFr = "لیست کاربران"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "GetClaimList",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "GetClaimList",
                ActionTitleFr = "دریافت لیست دسترسی ها"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "GetUserClaims",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "GetUserClaims",
                ActionTitleFr = "لیست دسترسی های کاربر"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "AssignRoleToClaims",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "AssignRoleToClaims",
                ActionTitleFr = "اختصاص دسترسی به نقش"
            };
            costsActions.Add(costChild1);
            costChild1 = new Claims()
            {
                Id = id++,
                ControllerName = "AuthManagement",
                ActionName = "RemoveClaimsFromRole",
                ControllerEntityID = 4,
                ControlleEnTitile = "AuthManagement",
                ControlleFaTitile = "مدیریت کاربران",
                ActionTitleEn = "RemoveClaimsFromRole",
                ActionTitleFr = "حذف دسترسی از نقش"
            };
            costsActions.Add(costChild1);
            modelBuilder.Entity<Claims>().HasData(costsActions);
            return id;
        }
    }
}
