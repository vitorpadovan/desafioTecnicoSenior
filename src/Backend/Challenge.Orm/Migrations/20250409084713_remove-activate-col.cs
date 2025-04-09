using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenge.Orm.Migrations
{
    /// <inheritdoc />
    public partial class removeactivatecol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isactive",
                table: "product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
