using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPlaneta.PGDB.Migrations
{
    /// <inheritdoc />
    public partial class M1ChangeUseraddColumlogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");
        }
    }
}
