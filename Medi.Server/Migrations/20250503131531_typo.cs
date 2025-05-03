using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medi.Server.Migrations
{
    /// <inheritdoc />
    public partial class typo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApartamentNumber",
                table: "Address",
                newName: "ApartmentNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApartmentNumber",
                table: "Address",
                newName: "ApartamentNumber");
        }
    }
}
