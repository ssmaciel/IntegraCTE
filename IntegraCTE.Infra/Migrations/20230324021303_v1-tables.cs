using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegraCTE.Infra.Migrations
{
    public partial class v1tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XML = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    Processado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DataArquivo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Integrado = table.Column<bool>(type: "bit", nullable: false),
                    DataIntegracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transportadoras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CodigoExterno = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportadoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTEs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notas = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Site = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    NumeroCte = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    SerieCte = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ChaveAcessoCte = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    OrdemCompra = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    TransportadoraID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinatarioCNPJCPF = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioNome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioLogradouro = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioNro = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioBairro = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioCodigoMunicipio = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioMunicipio = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioCEP = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioUF = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioCodigoPais = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DestinatarioPais = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CNPJRemetente = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CNPJEntidadeLegal = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ValorCte = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ModeloCte = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Justificativa = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TomadorServico = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    NotaFiscal = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ChaveNotaFiscal = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CFOP = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    UFEnv = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    UFEmitente = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    UFRemetente = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataArquivo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Integrado = table.Column<bool>(type: "bit", nullable: false),
                    DataIntegracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTEs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTEs_Transportadoras_TransportadoraID",
                        column: x => x.TransportadoraID,
                        principalTable: "Transportadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTEs_TransportadoraID",
                table: "CTEs",
                column: "TransportadoraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "CTEs");

            migrationBuilder.DropTable(
                name: "Transportadoras");
        }
    }
}
