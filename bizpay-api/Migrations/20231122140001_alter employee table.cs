using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bizpay_api.Migrations
{
    /// <inheritdoc />
    public partial class alteremployeetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyPayment",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HourlyPayment",
                table: "Employees",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
