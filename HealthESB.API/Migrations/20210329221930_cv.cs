using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthESB.API.Migrations
{
    public partial class cv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "ActionName", "ActionTitleEn", "ActionTitleFr", "ControlleEnTitile", "ControlleFaTitile", "ControllerEntityID", "ControllerName" },
                values: new object[] { 7, "GetPrescriptionBarcodeForActivation", "GetPrescriptionBarcodeForActivation", "جزئیات اقلام های نسخه های ارسالی", "PrescriptionBarcodeDetailes", "جزئیات اقلام نسخه", 3, "PrescriptionBarcodeDetailes" });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "ActionName", "ActionTitleEn", "ActionTitleFr", "ControlleEnTitile", "ControlleFaTitile", "ControllerEntityID", "ControllerName" },
                values: new object[] { 14, "getUsersAsync", "getUsersAsync", "لیست کاربران", "AuthManagement", "مدیریت کاربران", 4, "AuthManagement" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
