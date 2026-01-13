using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoeLeagueTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountName = table.Column<string>(type: "text", nullable: false),
                    IsTwitchLinked = table.Column<bool>(type: "boolean", nullable: false),
                    TwitchUsername = table.Column<string>(type: "text", nullable: true),
                    CompletedChallenges = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountName);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    ClassName = table.Column<int>(type: "integer", nullable: false),
                    Experience = table.Column<long>(type: "bigint", nullable: false),
                    Account = table.Column<string>(type: "text", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    Dead = table.Column<bool>(type: "boolean", nullable: false),
                    Retired = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    Depth = table.Column<int>(type: "integer", nullable: true),
                    AccountName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Accounts_AccountName",
                        column: x => x.AccountName,
                        principalTable: "Accounts",
                        principalColumn: "AccountName");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_AccountName",
                table: "Characters",
                column: "AccountName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
