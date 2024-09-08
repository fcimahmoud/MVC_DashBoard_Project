using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class renameProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Departments",
                newName: "DateOfCreation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfCreation",
                table: "Departments",
                newName: "MyProperty");
        }
    }
}
