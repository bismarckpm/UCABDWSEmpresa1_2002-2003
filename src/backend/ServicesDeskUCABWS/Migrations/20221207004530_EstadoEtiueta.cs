using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class EstadoEtiueta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Etiquetas_EtiquetaId",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_EtiquetaId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "EtiquetaId",
                table: "Estados");

            migrationBuilder.CreateTable(
                name: "EstadoEtiqueta",
                columns: table => new
                {
                    estadosid = table.Column<int>(type: "int", nullable: false),
                    etiquetasid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoEtiqueta", x => new { x.estadosid, x.etiquetasid });
                    table.ForeignKey(
                        name: "FK_EstadoEtiqueta_Estados_estadosid",
                        column: x => x.estadosid,
                        principalTable: "Estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstadoEtiqueta_Etiquetas_etiquetasid",
                        column: x => x.etiquetasid,
                        principalTable: "Etiquetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadoEtiqueta_etiquetasid",
                table: "EstadoEtiqueta",
                column: "etiquetasid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadoEtiqueta");

            migrationBuilder.AddColumn<int>(
                name: "EtiquetaId",
                table: "Estados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_EtiquetaId",
                table: "Estados",
                column: "EtiquetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Etiquetas_EtiquetaId",
                table: "Estados",
                column: "EtiquetaId",
                principalTable: "Etiquetas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
