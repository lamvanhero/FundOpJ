using Microsoft.EntityFrameworkCore.Migrations;

namespace OppJar.Domain.Migrations
{
    public partial class RenameDebitCreditIndicator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebitCreditIndicator",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "DbCrIndicator",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DbCrIndicator",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "DebitCreditIndicator",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
