using Microsoft.EntityFrameworkCore.Migrations;

namespace OppJar.Domain.Migrations
{
    public partial class RenamePortfolioField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorfolioId",
                table: "UserDetails");

            migrationBuilder.AddColumn<string>(
                name: "PortfolioId",
                table: "UserDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "UserDetails");

            migrationBuilder.AddColumn<string>(
                name: "PorfolioId",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
