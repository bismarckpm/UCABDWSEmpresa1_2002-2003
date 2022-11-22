using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class CambioModeloJerarquico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeloJerarquicoId",
                table: "TipoCargos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModeloJerarquicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloJerarquicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloJerarquicos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloParalelo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    paraleloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cantidadAprobaciones = table.Column<int>(type: "int", nullable: false),
                    categoriaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloParalelo", x => x.id);
                    table.ForeignKey(
                        name: "FK_ModeloParalelo_Categorias_categoriaid",
                        column: x => x.categoriaid,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlujoAprobacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketid = table.Column<int>(type: "int", nullable: true),
                    modelojerarquicoid = table.Column<int>(type: "int", nullable: false),
                    modeloParaleloid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    usuarioid = table.Column<int>(type: "int", nullable: true),
                    secuencia = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlujoAprobacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_FlujoAprobacion_ModeloJerarquicos_modelojerarquicoid",
                        column: x => x.modelojerarquicoid,
                        principalTable: "ModeloJerarquicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlujoAprobacion_ModeloParalelo_modeloParaleloid",
                        column: x => x.modeloParaleloid,
                        principalTable: "ModeloParalelo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_FlujoAprobacion_Tickets_ticketid",
                        column: x => x.ticketid,
                        principalTable: "Tickets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_FlujoAprobacion_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoCargos_ModeloJerarquicoId",
                table: "TipoCargos",
                column: "ModeloJerarquicoId");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_modelojerarquicoid",
                table: "FlujoAprobacion",
                column: "modelojerarquicoid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_modeloParaleloid",
                table: "FlujoAprobacion",
                column: "modeloParaleloid");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_ticketid",
                table: "FlujoAprobacion",
                column: "ticketid");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_usuarioid",
                table: "FlujoAprobacion",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloJerarquicos_CategoriaId",
                table: "ModeloJerarquicos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloParalelo_categoriaid",
                table: "ModeloParalelo",
                column: "categoriaid");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoCargos_ModeloJerarquicos_ModeloJerarquicoId",
                table: "TipoCargos",
                column: "ModeloJerarquicoId",
                principalTable: "ModeloJerarquicos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoCargos_ModeloJerarquicos_ModeloJerarquicoId",
                table: "TipoCargos");

            migrationBuilder.DropTable(
                name: "FlujoAprobacion");

            migrationBuilder.DropTable(
                name: "ModeloJerarquicos");

            migrationBuilder.DropTable(
                name: "ModeloParalelo");

            migrationBuilder.DropIndex(
                name: "IX_TipoCargos_ModeloJerarquicoId",
                table: "TipoCargos");

            migrationBuilder.DropColumn(
                name: "ModeloJerarquicoId",
                table: "TipoCargos");
        }
    }
}
