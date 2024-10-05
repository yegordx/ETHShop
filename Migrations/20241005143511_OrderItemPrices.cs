using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETHShop.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "PriceETH",
                table: "OrderItems",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<double>(
                name: "PricePs",
                table: "OrderItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePs",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "OrderItems",
                newName: "PriceETH");

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "CartItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
