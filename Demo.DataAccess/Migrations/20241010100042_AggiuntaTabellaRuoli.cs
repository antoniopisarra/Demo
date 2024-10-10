using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaTabellaRuoli : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruoli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoRuolo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruoli", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ruoli_TipoRuolo",
                table: "Ruoli",
                column: "TipoRuolo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ruoli");
        }
    }
}
