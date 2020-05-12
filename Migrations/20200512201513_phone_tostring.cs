using Microsoft.EntityFrameworkCore.Migrations;

namespace PetManager.Migrations
{
    public partial class phone_tostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd106fe4-6088-44b4-a313-71743712598c");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "PetOwners",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7e4fe36-e522-4715-be09-fdd4d6bf5830", "e22caaa1-c219-45f7-8f58-3368f8064bcd", "Pet Owner", "PET OWNER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7e4fe36-e522-4715-be09-fdd4d6bf5830");

            migrationBuilder.AlterColumn<double>(
                name: "PhoneNumber",
                table: "PetOwners",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd106fe4-6088-44b4-a313-71743712598c", "f81d6b08-4085-48e3-8b3c-b3fdcd076318", "Pet Owner", "PET OWNER" });
        }
    }
}
