using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestTask.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment_Types",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment_Types", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Product_Facilities",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Facilities", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductFacilityCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EquipmentTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EquipmentCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Equipment_Types_EquipmentTypeCode",
                        column: x => x.EquipmentTypeCode,
                        principalTable: "Equipment_Types",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Product_Facilities_ProductFacilityCode",
                        column: x => x.ProductFacilityCode,
                        principalTable: "Product_Facilities",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipment_Types",
                columns: new[] { "Code", "Area", "Id", "Name" },
                values: new object[,]
                {
                    { "e001", 20.0, 0, "EquipmentA" },
                    { "e002", 15.0, 0, "EquipmentB" },
                    { "e003", 5.0, 0, "EquipmentC" },
                    { "e004", 40.0, 0, "EquipmentD" }
                });

            migrationBuilder.InsertData(
                table: "Product_Facilities",
                columns: new[] { "Code", "Area", "Id", "Name" },
                values: new object[,]
                {
                    { "p001", 100.0, 0, "ProductA" },
                    { "p002", 75.0, 0, "ProductB" },
                    { "p003", 50.0, 0, "ProductC" },
                    { "p004", 25.0, 0, "ProductD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EquipmentTypeCode",
                table: "Contracts",
                column: "EquipmentTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ProductFacilityCode",
                table: "Contracts",
                column: "ProductFacilityCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Equipment_Types");

            migrationBuilder.DropTable(
                name: "Product_Facilities");
        }
    }
}
