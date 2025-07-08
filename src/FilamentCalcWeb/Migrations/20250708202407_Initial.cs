using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FilamentCalculator.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilamentTypes",
                columns: table => new
                {
                    FilamentTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    WeightPerMM = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilamentTypes", x => x.FilamentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ManufacturerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerId);
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    PrinterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ManufacturerName = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    PeriotOfAmortisation = table.Column<decimal>(type: "numeric", nullable: false),
                    FilamentDiameter = table.Column<float>(type: "real", nullable: false),
                    EnergyConsumptionW = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.PrinterId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Energiekosts = table.Column<decimal>(type: "numeric", nullable: false),
                    MissprintChance = table.Column<int>(type: "integer", nullable: false),
                    PrinterDepricationKostsPerHour = table.Column<decimal>(type: "numeric", nullable: false),
                    Hourlywage = table.Column<decimal>(type: "numeric", nullable: false),
                    Revenuepercentage = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingsId);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ShipmentOrg = table.Column<string>(type: "text", nullable: true),
                    Packagingprice = table.Column<decimal>(type: "numeric", nullable: false),
                    FillerPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    LablePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    AddonItemPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ShippingPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentID);
                });

            migrationBuilder.CreateTable(
                name: "Filaments",
                columns: table => new
                {
                    FilamentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilamentTypeId = table.Column<int>(type: "integer", nullable: false),
                    ManufacturerId = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Diameter = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    SpoolWeight = table.Column<float>(type: "real", nullable: false),
                    PrintTempNozzle = table.Column<string>(type: "text", nullable: true),
                    PrintTempBed = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filaments", x => x.FilamentId);
                    table.ForeignKey(
                        name: "FK_Filaments_FilamentTypes_FilamentTypeId",
                        column: x => x.FilamentTypeId,
                        principalTable: "FilamentTypes",
                        principalColumn: "FilamentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filaments_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "ManufacturerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filaments_FilamentTypeId",
                table: "Filaments",
                column: "FilamentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Filaments_ManufacturerId",
                table: "Filaments",
                column: "ManufacturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filaments");

            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "FilamentTypes");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
