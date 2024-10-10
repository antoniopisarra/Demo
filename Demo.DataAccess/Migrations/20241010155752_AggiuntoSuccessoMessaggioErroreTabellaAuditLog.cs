using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntoSuccessoMessaggioErroreTabellaAuditLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessaggioErrore",
                table: "AuditLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Successo",
                table: "AuditLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessaggioErrore",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Successo",
                table: "AuditLogs");
        }
    }
}
