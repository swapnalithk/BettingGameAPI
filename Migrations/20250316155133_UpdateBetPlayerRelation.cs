using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BettingGameAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBetPlayerRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Bets");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Bets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bets_PlayerId",
                table: "Bets",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Players_PlayerId",
                table: "Bets",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Players_PlayerId",
                table: "Bets");

            migrationBuilder.DropIndex(
                name: "IX_Bets_PlayerId",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Bets");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Bets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
