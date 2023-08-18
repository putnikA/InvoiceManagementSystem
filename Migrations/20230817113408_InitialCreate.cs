using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoiceManagementWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SupplierAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    InvoiceNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceModel_SupplierModel_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "SupplierModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceModel_SupplierId",
                table: "InvoiceModel",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceModel");

            migrationBuilder.DropTable(
                name: "SupplierModel");
        }
    }
}
