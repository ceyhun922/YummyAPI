using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "messageBox",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "messageBox",
                table: "Contacts");
        }
    }
}
