using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ardaporno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubCards_Discounts_discountId",
                table: "ClubCards");

            migrationBuilder.DropIndex(
                name: "IX_ClubCards_discountId",
                table: "ClubCards");

            migrationBuilder.DropColumn(
                name: "discountId",
                table: "ClubCards");

            migrationBuilder.AddColumn<int>(
                name: "CardType",
                table: "ClubCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardType",
                table: "ClubCards");

            migrationBuilder.AddColumn<long>(
                name: "discountId",
                table: "ClubCards",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ClubCards_discountId",
                table: "ClubCards",
                column: "discountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubCards_Discounts_discountId",
                table: "ClubCards",
                column: "discountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
