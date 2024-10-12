using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETHShop.Migrations
{
    /// <inheritdoc />
    public partial class OrderAndAddressRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShippingAddressID",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressID",
                table: "Orders",
                column: "ShippingAddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressID",
                table: "Orders",
                column: "ShippingAddressID",
                principalTable: "ShippingAddresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingAddressID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddressID",
                table: "Orders");
        }
    }
}
