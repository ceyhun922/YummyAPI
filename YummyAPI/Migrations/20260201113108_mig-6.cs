using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupPriority",
                table: "GroupOrganizationChefs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupPriority",
                table: "GroupOrganizationChefs");
        }
    }
}
