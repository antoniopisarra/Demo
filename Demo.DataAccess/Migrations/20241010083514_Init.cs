using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoEvento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeTabella = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChiavePrimaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValoriPrecedenti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NuoviValori = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Utente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataModifica = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");
        }
    }
}
