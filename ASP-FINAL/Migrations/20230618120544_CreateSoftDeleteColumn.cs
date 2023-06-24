using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_FINAL.Migrations
{
    public partial class CreateSoftDeleteColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDelete",
                table: "Slider",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDelete",
                table: "Slider");
        }
    }
}
