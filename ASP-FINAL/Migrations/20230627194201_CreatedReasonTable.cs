using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_FINAL.Migrations
{
    public partial class CreatedReasonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Reasons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Reasons");
        }
    }
}
