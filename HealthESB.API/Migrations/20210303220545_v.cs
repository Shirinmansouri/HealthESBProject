using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthESB.API.Migrations
{
    public partial class v : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecheckCode",
                table: "PrescriptionBarcode",
                newName: "ReCheckCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReCheckCode",
                table: "PrescriptionBarcode",
                newName: "RecheckCode");
        }
    }
}
