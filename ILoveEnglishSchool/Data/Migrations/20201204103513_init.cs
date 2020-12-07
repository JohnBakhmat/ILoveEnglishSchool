using Microsoft.EntityFrameworkCore.Migrations;

namespace ILoveEnglishSchool.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LessonNumber = table.Column<int>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WelcomeImage = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LessonId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_Parts_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartModulesEnumerable",
                columns: table => new
                {
                    ModuleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PartId = table.Column<int>(nullable: false),
                    Header = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ContentUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartModulesEnumerable", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_PartModulesEnumerable_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartModulesEnumerable_PartId",
                table: "PartModulesEnumerable",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_LessonId",
                table: "Parts",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartModulesEnumerable");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Lessons");
        }
    }
}
