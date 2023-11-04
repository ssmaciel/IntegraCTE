using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegraCTE.Infra.Migrations
{
    public partial class v4validacoescampomensagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mensagem",
                table: "Validacao",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mensagem",
                table: "Validacao",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
