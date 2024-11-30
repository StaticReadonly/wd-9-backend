using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.DbConn.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "First_name",
                table: "Users",
                newName: "First_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "First_Name",
                table: "Users",
                newName: "First_name");
        }
    }
}
