using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Units",
                newName: "NamaUnit");

            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Units",
                newName: "Kapasitas");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Units",
                newName: "JenisUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NamaUnit",
                table: "Units",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Kapasitas",
                table: "Units",
                newName: "ISBN");

            migrationBuilder.RenameColumn(
                name: "JenisUnit",
                table: "Units",
                newName: "Author");
        }
    }
}
