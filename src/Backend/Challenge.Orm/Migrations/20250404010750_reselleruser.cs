using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Challenge.Orm.Migrations
{
    /// <inheritdoc />
    public partial class reselleruser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "resellerusers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resellerusers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resellerusers_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "resselertouser",
                columns: table => new
                {
                    resellerid = table.Column<Guid>(type: "uuid", nullable: false),
                    resellerusersid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resselertouser", x => new { x.resellerid, x.resellerusersid });
                    table.ForeignKey(
                        name: "FK_resselertouser_reseller_resellerid",
                        column: x => x.resellerid,
                        principalTable: "reseller",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_resselertouser_resellerusers_resellerusersid",
                        column: x => x.resellerusersid,
                        principalTable: "resellerusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_resellerusers_UsersId",
                table: "resellerusers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_resselertouser_resellerusersid",
                table: "resselertouser",
                column: "resellerusersid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resselertouser");

            migrationBuilder.DropTable(
                name: "resellerusers");
        }
    }
}
