using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class cambio_tabla_estado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModeloJerarquicoCargos_TipoCargos_TipoCargoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropTable(
                name: "EstadoEtiqueta");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCargoid",
                table: "ModeloJerarquicoCargos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloJerarquicoCargos_TipoCargos_TipoCargoid",
                table: "ModeloJerarquicoCargos",
                column: "TipoCargoid",
                principalTable: "TipoCargos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Etiquetas_EtiquetaId",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloJerarquicoCargos_TipoCargos_TipoCargoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropIndex(
                name: "IX_Estados_EtiquetaId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "EtiquetaId",
                table: "Estados");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCargoid",
                table: "ModeloJerarquicoCargos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloJerarquicoCargos_TipoCargos_TipoCargoid",
                table: "ModeloJerarquicoCargos",
                column: "TipoCargoid",
                principalTable: "TipoCargos",
                principalColumn: "id");
        }
    }
}
