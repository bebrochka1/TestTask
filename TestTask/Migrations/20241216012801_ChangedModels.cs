using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations
{
    /// <inheritdoc />
    public partial class ChangedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Product_Facilities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Equipment_Types");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Product_Facilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Equipment_Types",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Equipment_Types",
                keyColumn: "Code",
                keyValue: "e001",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Equipment_Types",
                keyColumn: "Code",
                keyValue: "e002",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Equipment_Types",
                keyColumn: "Code",
                keyValue: "e003",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Equipment_Types",
                keyColumn: "Code",
                keyValue: "e004",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product_Facilities",
                keyColumn: "Code",
                keyValue: "p001",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product_Facilities",
                keyColumn: "Code",
                keyValue: "p002",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product_Facilities",
                keyColumn: "Code",
                keyValue: "p003",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product_Facilities",
                keyColumn: "Code",
                keyValue: "p004",
                column: "Id",
                value: 0);
        }
    }
}
