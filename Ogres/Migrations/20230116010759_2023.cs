using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ogres.Migrations
{
    /// <inheritdoc />
    public partial class _2023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypePlats",
                columns: table => new
                {
                    TypePlatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Probabilite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePlats", x => x.TypePlatId);
                });

            migrationBuilder.CreateTable(
                name: "Plats",
                columns: table => new
                {
                    PlatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Taille = table.Column<int>(type: "int", nullable: false),
                    TypePlatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plats", x => x.PlatId);
                    table.ForeignKey(
                        name: "FK_Plats_TypePlats_TypePlatId",
                        column: x => x.TypePlatId,
                        principalTable: "TypePlats",
                        principalColumn: "TypePlatId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plats_TypePlatId",
                table: "Plats",
                column: "TypePlatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plats");

            migrationBuilder.DropTable(
                name: "TypePlats");
        }
    }
}
