using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopLibrary.Migrations
{
    /// <inheritdoc />
    public partial class fixClubCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DiscountId",
                table: "ClubCards",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ClubCards_DiscountId",
                table: "ClubCards",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubCards_Discounts_DiscountId",
                table: "ClubCards",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubCards_Discounts_DiscountId",
                table: "ClubCards");

            migrationBuilder.DropIndex(
                name: "IX_ClubCards_DiscountId",
                table: "ClubCards");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "ClubCards");
        }
    }
}
