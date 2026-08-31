using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CajaAhorro.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ahorros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    TotalNumeros = table.Column<int>(type: "INTEGER", nullable: false),
                    MontoPorNumero = table.Column<decimal>(type: "TEXT", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaPagoParticipantes = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Finalizada = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ahorros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetallesAhorros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AhorroId = table.Column<int>(type: "INTEGER", nullable: false),
                    SocioId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroAsignado = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaCobro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MontoAEntregar = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesAhorros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesAhorros_Ahorros_AhorroId",
                        column: x => x.AhorroId,
                        principalTable: "Ahorros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAhorros_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAhorros_AhorroId",
                table: "DetallesAhorros",
                column: "AhorroId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAhorros_SocioId",
                table: "DetallesAhorros",
                column: "SocioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesAhorros");

            migrationBuilder.DropTable(
                name: "Ahorros");

            migrationBuilder.DropTable(
                name: "Socios");
        }
    }
}
