using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaTabellaUtentiRuoli : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UtentiRuoli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    IdRuolo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtentiRuoli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtentiRuoli_Ruoli_IdRuolo",
                        column: x => x.IdRuolo,
                        principalTable: "Ruoli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtentiRuoli_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtentiRuoli_IdRuolo",
                table: "UtentiRuoli",
                column: "IdRuolo");

            migrationBuilder.CreateIndex(
                name: "IX_UtentiRuoli_IdUtente",
                table: "UtentiRuoli",
                column: "IdUtente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtentiRuoli");
        }
    }
}
