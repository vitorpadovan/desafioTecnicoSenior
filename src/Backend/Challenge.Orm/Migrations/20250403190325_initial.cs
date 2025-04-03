using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Challenge.Orm.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reseller",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    document = table.Column<string>(type: "text", nullable: false),
                    registredname = table.Column<string>(type: "text", nullable: false),
                    tradename = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reseller", x => x.id);
                    table.UniqueConstraint("AK_reseller_document", x => x.document);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    postalcode = table.Column<int>(type: "integer", nullable: false),
                    resellerdocumentid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_reseller_resellerdocumentid",
                        column: x => x.resellerdocumentid,
                        principalTable: "reseller",
                        principalColumn: "document",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contact",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResellerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact", x => x.id);
                    table.ForeignKey(
                        name: "FK_contact_reseller_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "reseller",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_resellerdocumentid",
                table: "address",
                column: "resellerdocumentid");

            migrationBuilder.CreateIndex(
                name: "IX_contact_ResellerId",
                table: "contact",
                column: "ResellerId");

            migrationBuilder.CreateIndex(
                name: "IX_reseller_document",
                table: "reseller",
                column: "document",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "contact");

            migrationBuilder.DropTable(
                name: "reseller");
        }
    }
}
