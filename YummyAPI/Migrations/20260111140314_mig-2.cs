using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipationRate",
                table: "GroupOrganizations");

            migrationBuilder.AddColumn<int>(
                name: "ParticipantCount",
                table: "GroupOrganizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonCount",
                table: "GroupOrganizations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantCount",
                table: "GroupOrganizations");

            migrationBuilder.DropColumn(
                name: "PersonCount",
                table: "GroupOrganizations");

            migrationBuilder.AddColumn<string>(
                name: "ParticipationRate",
                table: "GroupOrganizations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
