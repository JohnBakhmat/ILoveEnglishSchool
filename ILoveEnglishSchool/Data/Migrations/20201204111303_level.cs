using Microsoft.EntityFrameworkCore.Migrations;

namespace ILoveEnglishSchool.Data.Migrations
{
    public partial class level : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Lessons");
        }
    }
}
