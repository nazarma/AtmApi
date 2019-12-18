using Microsoft.EntityFrameworkCore.Migrations;

namespace Atm.Persistence.Migrations
{
    public partial class AddedTenderTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenderType",
                table: "LegalTender",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenderType",
                table: "LegalTender");
        }
    }
}
