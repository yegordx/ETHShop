﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETHShop.Migrations
{
    /// <inheritdoc />
    public partial class CategorySoftDelete2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Categories");
        }
    }
}
