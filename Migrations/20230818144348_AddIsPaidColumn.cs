using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoiceManagementWebApp.Migrations
{
    public partial class AddIsPaidColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "InvoiceModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceModel_SelectedSupplierId",
                table: "InvoiceModel",
                column: "SelectedSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceModel_SupplierModel_SelectedSupplierId",
                table: "InvoiceModel",
                column: "SelectedSupplierId",
                principalTable: "SupplierModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceModel_SupplierModel_SelectedSupplierId",
                table: "InvoiceModel");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceModel_SelectedSupplierId",
                table: "InvoiceModel");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "InvoiceModel");
        }
    }
}
