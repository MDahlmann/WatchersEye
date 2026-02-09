using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoeLeagueTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeagueEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeagueName",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueName);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LeagueName",
                table: "Accounts",
                column: "LeagueName");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Leagues_LeagueName",
                table: "Accounts",
                column: "LeagueName",
                principalTable: "Leagues",
                principalColumn: "LeagueName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Leagues_LeagueName",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LeagueName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LeagueName",
                table: "Accounts");
        }
    }
}
