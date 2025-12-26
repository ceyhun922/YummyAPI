using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    AboutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutSubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutTitleChecked1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutTitleChecked2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutTitleChecked3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutDesciription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutVideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.AboutId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");
        }
    }
}
