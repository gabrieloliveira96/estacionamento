using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.Infra.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_VAGA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroVaga = table.Column<string>(type: "varchar(10)", nullable: false),
                    TipoVaga = table.Column<string>(type: "varchar(10)", nullable: false),
                    Ocupada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_VAGA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_REGISTRO_VEICULO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Marca = table.Column<string>(type: "varchar(30)", nullable: false),
                    Modelo = table.Column<string>(type: "varchar(30)", nullable: false),
                    Ano = table.Column<string>(type: "varchar(4)", nullable: false),
                    Cor = table.Column<string>(type: "varchar(15)", nullable: false),
                    Placa = table.Column<string>(type: "varchar(10)", nullable: false),
                    TipoVeiculo = table.Column<string>(type: "varchar(10)", nullable: false),
                    VagaId = table.Column<int>(type: "int", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_REGISTRO_VEICULO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_REGISTRO_VEICULO_T_VAGA_VagaId",
                        column: x => x.VagaId,
                        principalTable: "T_VAGA",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 1, "01", 0, "Media" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 2, "02", 0, "Media" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 3, "03", 0, "Media" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 4, "04", 0, "Media" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 5, "05", 0, "Pequena" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 6, "06", 0, "Pequena" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 7, "07", 0, "Pequena" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 8, "08", 0, "Pequena" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 9, "09", 0, "Grande" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 10, "10", 0, "Grande" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 11, "11", 0, "Grande" });

            migrationBuilder.InsertData(
                table: "T_VAGA",
                columns: new[] { "Id", "NumeroVaga", "Ocupada", "TipoVaga" },
                values: new object[] { 12, "12", 0, "Grande" });

            migrationBuilder.CreateIndex(
                name: "IX_T_REGISTRO_VEICULO_VagaId",
                table: "T_REGISTRO_VEICULO",
                column: "VagaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_REGISTRO_VEICULO");

            migrationBuilder.DropTable(
                name: "T_VAGA");
        }
    }
}
