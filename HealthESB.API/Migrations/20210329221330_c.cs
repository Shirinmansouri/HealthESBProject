using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthESB.API.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Claims",
                newName: "ActionName");

            migrationBuilder.AddColumn<string>(
                name: "ActionTitleEn",
                table: "Claims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionTitleFr",
                table: "Claims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControlleEnTitile",
                table: "Claims",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ControlleFaTitile",
                table: "Claims",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ControllerEntityID",
                table: "Claims",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Claims",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "ActionName", "ActionTitleEn", "ActionTitleFr", "ControlleEnTitile", "ControlleFaTitile", "ControllerEntityID", "ControllerName" },
                values: new object[,]
                {
                    { 1, "Create", "Create", "ایجاد", "PrescriptionBarcode", "اقلام نسخه", 2, "PrescriptionBarcode" },
                    { 2, "ReActiveUid", "ReActiveUid", "فعال سازی مجدد", "PrescriptionBarcode", "اقلام نسخه", 2, "PrescriptionBarcode" },
                    { 3, "confirm", "confirm", "تایید نهایی اقلام", "PrescriptionBarcode", "اقلام نسخه", 2, "PrescriptionBarcode" },
                    { 4, "Create", "Create", "ایجاد نسخه", "Prescription", "نسخه", 1, "Prescription" },
                    { 5, "ReActiveByPrescriptionId", "ReActiveByPrescriptionId", "فعال سازی گروهی با شماره نسخه", "PrescriptionBarcode", "اقلام نسخه", 2, "PrescriptionBarcode" },
                    { 6, "GetPrescriptionActivity", "GetPrescriptionActivity", "تاریخچه ی درخواست های ارسالی", "PrescriptionBarcodeDetailes", "جزئیات اقلام نسخه", 3, "PrescriptionBarcodeDetailes" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "ActionTitleEn",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ActionTitleFr",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ControlleEnTitile",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ControlleFaTitile",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ControllerEntityID",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "ActionName",
                table: "Claims",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Claims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
