using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashRegister.Infrastructure.Migrations
{
    public partial class ProductBillModelCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Bills_BillId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BillId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductIds",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bills",
                newName: "BillNumber");

            migrationBuilder.CreateTable(
                name: "BillProducts",
                columns: table => new
                {
                    BillNumber = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ProductQuantity = table.Column<int>(type: "integer", nullable: false),
                    ProductsPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillProducts", x => new { x.BillNumber, x.ProductId });
                    table.ForeignKey(
                        name: "FK_BillProducts_Bills_BillNumber",
                        column: x => x.BillNumber,
                        principalTable: "Bills",
                        principalColumn: "BillNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillProducts_ProductId",
                table: "BillProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillProducts");

            migrationBuilder.RenameColumn(
                name: "BillNumber",
                table: "Bills",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<int>>(
                name: "ProductIds",
                table: "Bills",
                type: "integer[]",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BillId",
                table: "Products",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Bills_BillId",
                table: "Products",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
