using Microsoft.EntityFrameworkCore.Migrations;

namespace PetManager.Migrations
{
    public partial class added_phonenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e6d1de8-8787-47be-a3f6-da2e5a400b1d");

            migrationBuilder.AddColumn<double>(
                name: "PhoneNumber",
                table: "PetOwners",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd106fe4-6088-44b4-a313-71743712598c", "f81d6b08-4085-48e3-8b3c-b3fdcd076318", "Pet Owner", "PET OWNER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd106fe4-6088-44b4-a313-71743712598c");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PetOwners");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e6d1de8-8787-47be-a3f6-da2e5a400b1d", "2e3b4fba-c7cc-4036-beee-0404a9c52944", "Pet Owner", "PET OWNER" });
        }
    }
}
