using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenge.Orm.Migrations
{
    /// <inheritdoc />
    public partial class orderitemstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "state",
                table: "orderdetail",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "orderdetail");
        }
    }
}
