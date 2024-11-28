using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.DbConn.Migrations
{
    /// <inheritdoc />
    public partial class amountfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Components",
                type: "numeric(10,3)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Components",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(10,3)");
        }
    }
}
