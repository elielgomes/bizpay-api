using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bizpay_api.Migrations
{
    /// <inheritdoc />
    public partial class addtablepayslip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payslips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateOfIssue = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Discounts = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Inss = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Irrf = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payslips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payslips_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Payslips_EmployeeId",
                table: "Payslips",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payslips");
        }
    }
}
