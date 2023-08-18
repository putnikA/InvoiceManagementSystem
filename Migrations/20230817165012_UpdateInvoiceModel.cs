using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoiceManagementWebApp.Migrations
{
    public partial class UpdateInvoiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceModel_SupplierModel_SupplierId",
                table: "InvoiceModel");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceModel_SupplierId",
                table: "InvoiceModel");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "InvoiceModel");

            migrationBuilder.AddColumn<int>(
                name: "SelectedSupplierId",
                table: "InvoiceModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedSupplierId",
                table: "InvoiceModel");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "InvoiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceModel_SupplierId",
                table: "InvoiceModel",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceModel_SupplierModel_SupplierId",
                table: "InvoiceModel",
                column: "SupplierId",
                principalTable: "SupplierModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
