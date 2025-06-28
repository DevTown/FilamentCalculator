using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilamentCalculator.Migrations
{
    /// <inheritdoc />
    public partial class fix_Missing_stuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ShippingPrice",
                table: "Shipments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingPrice",
                table: "Shipments");
        }
    }
}
