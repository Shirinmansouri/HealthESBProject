using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthESB.API.Migrations
{
    public partial class tt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "ActionName", "ActionTitleEn", "ActionTitleFr", "ControlleEnTitile", "ControlleFaTitile", "ControllerEntityID", "ControllerName" },
                values: new object[,]
                {
                    { 8, "CreateUser", "CreateUser", "ایجاد کاربر", "AuthManagement", "مدیریت کاربران", 4, "AuthManagement" },
                    { 9, "UpdateUser", "UpdateUser", "به روززسانی کاربر", "AuthManagement", "مدیریت کاربران", 4, "AuthManagement" },
                    { 10, "CreateRoles", "CreateRoles", "ایجاد نقش", "AuthManagement", "مدیریت کاربران", 4, "AuthManagement" },
                    { 11, "UpdateRoles", "UpdateRoles", "به روزرسانی نقش", "AuthManagement", "مدیریت کاربران", 4, "AuthManagement" },
                    { 12, "GetRoles", "GetRoles", "لیست نقش ها", "AuthManagement", "مدیریت کاربران", 4, "AuthManagement" },
                    { 13, "getUserRolesByUserIdAsync", "getUserRolesByUserIdAsync", "دریافت نقش های کاربر", "AuthManagement", "مدیریت کاربران", 4, "AuthManagement" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
