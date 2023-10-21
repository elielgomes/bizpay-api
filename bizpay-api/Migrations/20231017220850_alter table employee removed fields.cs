using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bizpay_api.Migrations
{
    /// <inheritdoc />
    public partial class altertableemployeeremovedfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnerName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartnerName",
                table: "Employees",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "Employees",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
