using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegraCTE.Infra.Migrations
{
    public partial class v2altertransportadoracampospagamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalendarioPagamento",
                table: "Transportadoras",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EspecificacaoMetodoPagamento",
                table: "Transportadoras",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetodoPagamento",
                table: "Transportadoras",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalendarioPagamento",
                table: "Transportadoras");

            migrationBuilder.DropColumn(
                name: "EspecificacaoMetodoPagamento",
                table: "Transportadoras");

            migrationBuilder.DropColumn(
                name: "MetodoPagamento",
                table: "Transportadoras");
        }
    }
}
