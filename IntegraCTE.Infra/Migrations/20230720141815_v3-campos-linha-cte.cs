using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegraCTE.Infra.Migrations
{
    public partial class v3camposlinhacte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CFOPCode",
                table: "CTEs",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemNumber",
                table: "CTEs",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LineNumber",
                table: "CTEs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "CTEs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PurchasePriceQuantity",
                table: "CTEs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "dataAreaId",
                table: "CTEs",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Empresa",
                table: "Arquivos",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CFOPCode",
                table: "CTEs");

            migrationBuilder.DropColumn(
                name: "ItemNumber",
                table: "CTEs");

            migrationBuilder.DropColumn(
                name: "LineNumber",
                table: "CTEs");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "CTEs");

            migrationBuilder.DropColumn(
                name: "PurchasePriceQuantity",
                table: "CTEs");

            migrationBuilder.DropColumn(
                name: "dataAreaId",
                table: "CTEs");

            migrationBuilder.DropColumn(
                name: "Empresa",
                table: "Arquivos");
        }
    }
}
