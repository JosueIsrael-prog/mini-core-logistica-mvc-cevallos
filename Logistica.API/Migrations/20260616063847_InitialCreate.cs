using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Logistica.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "repartidores",
                columns: table => new
                {
                    id_repartidor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_repartidores", x => x.id_repartidor);
                });

            migrationBuilder.CreateTable(
                name: "zonas",
                columns: table => new
                {
                    id_zona = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_zona = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tarifa_por_kg = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zonas", x => x.id_zona);
                });

            migrationBuilder.CreateTable(
                name: "envios",
                columns: table => new
                {
                    id_envio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_repartidor = table.Column<int>(type: "integer", nullable: false),
                    id_zona = table.Column<int>(type: "integer", nullable: false),
                    peso_kg = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    fecha_envio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_envios", x => x.id_envio);
                    table.ForeignKey(
                        name: "FK_envios_repartidores_id_repartidor",
                        column: x => x.id_repartidor,
                        principalTable: "repartidores",
                        principalColumn: "id_repartidor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_envios_zonas_id_zona",
                        column: x => x.id_zona,
                        principalTable: "zonas",
                        principalColumn: "id_zona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "repartidores",
                columns: new[] { "id_repartidor", "email", "nombre" },
                values: new object[,]
                {
                    { 1, "andres@logistica.com", "Andrés" },
                    { 2, "camila@logistica.com", "Camila" },
                    { 3, "luis@logistica.com", "Luis" }
                });

            migrationBuilder.InsertData(
                table: "zonas",
                columns: new[] { "id_zona", "nombre_zona", "tarifa_por_kg" },
                values: new object[,]
                {
                    { 1, "Norte", 1.50m },
                    { 2, "Sur", 2.00m },
                    { 3, "Centro", 1.75m }
                });

            migrationBuilder.InsertData(
                table: "envios",
                columns: new[] { "id_envio", "fecha_envio", "id_repartidor", "id_zona", "peso_kg" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, 10.00m },
                    { 2, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, 12.00m },
                    { 3, new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, 10.00m },
                    { 4, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, 2, 8.00m },
                    { 5, new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc), 2, 2, 10.00m },
                    { 6, new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), 1, 3, 5.50m },
                    { 7, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), 2, 1, 7.00m },
                    { 8, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), 3, 1, 15.00m },
                    { 9, new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, 6.00m },
                    { 10, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, 3, 9.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_repartidor",
                table: "envios",
                column: "id_repartidor");

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_zona",
                table: "envios",
                column: "id_zona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "envios");

            migrationBuilder.DropTable(
                name: "repartidores");

            migrationBuilder.DropTable(
                name: "zonas");
        }
    }
}
