using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPlaneta.PGDB.Migrations
{
    /// <inheritdoc />
    public partial class M2ChangeIDUserforSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sessions");
        }
    }
}
