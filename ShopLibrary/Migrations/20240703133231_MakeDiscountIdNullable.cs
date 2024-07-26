using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopLibrary.Migrations
{
    /// <inheritdoc />
    public partial class MakeDiscountIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_discountId",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "discountId",
                table: "Products",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_discountId",
                table: "Products",
                column: "discountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_discountId",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "discountId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_discountId",
                table: "Products",
                column: "discountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
