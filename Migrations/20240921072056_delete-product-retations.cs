using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETHShop.Migrations
{
    /// <inheritdoc />
    public partial class deleteproductretations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Users_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShoppingCartID",
                table: "Users",
                column: "ShoppingCartID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ShoppingCarts_ShoppingCartID",
                table: "Users",
                column: "ShoppingCartID",
                principalTable: "ShoppingCarts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ShoppingCarts_ShoppingCartID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShoppingCartID",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Users_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
