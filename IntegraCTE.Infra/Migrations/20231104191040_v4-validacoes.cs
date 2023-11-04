using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegraCTE.Infra.Migrations
{
    public partial class v4validacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Validacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdArquivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensagem = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    TipoMensagem = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Validacao_Arquivos_IdArquivo",
                        column: x => x.IdArquivo,
                        principalTable: "Arquivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Validacao_IdArquivo",
                table: "Validacao",
                column: "IdArquivo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Validacao");
        }
    }
}
