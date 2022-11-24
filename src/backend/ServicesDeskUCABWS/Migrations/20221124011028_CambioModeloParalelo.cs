using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class CambioModeloParalelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobacion_ModeloParalelo_modeloParaleloid",
                table: "FlujoAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloParalelo_Categorias_categoriaid",
                table: "ModeloParalelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModeloParalelo",
                table: "ModeloParalelo");

            migrationBuilder.DropIndex(
                name: "IX_FlujoAprobacion_modeloParaleloid",
                table: "FlujoAprobacion");

            migrationBuilder.DropColumn(
                name: "id",
                table: "ModeloParalelo");

            migrationBuilder.DropColumn(
                name: "paraleloId",
                table: "ModeloParalelo");

            migrationBuilder.DropColumn(
                name: "modeloParaleloid",
                table: "FlujoAprobacion");

            migrationBuilder.RenameColumn(
                name: "categoriaid",
                table: "ModeloParalelo",
                newName: "categoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_ModeloParalelo_categoriaid",
                table: "ModeloParalelo",
                newName: "IX_ModeloParalelo_categoriaId");

            migrationBuilder.AddColumn<int>(
                name: "paraid",
                table: "ModeloParalelo",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "modeloParaleloparaid",
                table: "FlujoAprobacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "paraleloid",
                table: "FlujoAprobacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModeloParalelo",
                table: "ModeloParalelo",
                column: "paraid");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_modeloParaleloparaid",
                table: "FlujoAprobacion",
                column: "modeloParaleloparaid");

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobacion_ModeloParalelo_modeloParaleloparaid",
                table: "FlujoAprobacion",
                column: "modeloParaleloparaid",
                principalTable: "ModeloParalelo",
                principalColumn: "paraid");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloParalelo_Categorias_categoriaId",
                table: "ModeloParalelo",
                column: "categoriaId",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobacion_ModeloParalelo_modeloParaleloparaid",
                table: "FlujoAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloParalelo_Categorias_categoriaId",
                table: "ModeloParalelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModeloParalelo",
                table: "ModeloParalelo");

            migrationBuilder.DropIndex(
                name: "IX_FlujoAprobacion_modeloParaleloparaid",
                table: "FlujoAprobacion");

            migrationBuilder.DropColumn(
                name: "paraid",
                table: "ModeloParalelo");

            migrationBuilder.DropColumn(
                name: "modeloParaleloparaid",
                table: "FlujoAprobacion");

            migrationBuilder.DropColumn(
                name: "paraleloid",
                table: "FlujoAprobacion");

            migrationBuilder.RenameColumn(
                name: "categoriaId",
                table: "ModeloParalelo",
                newName: "categoriaid");

            migrationBuilder.RenameIndex(
                name: "IX_ModeloParalelo_categoriaId",
                table: "ModeloParalelo",
                newName: "IX_ModeloParalelo_categoriaid");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "ModeloParalelo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "paraleloId",
                table: "ModeloParalelo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "modeloParaleloid",
                table: "FlujoAprobacion",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModeloParalelo",
                table: "ModeloParalelo",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_modeloParaleloid",
                table: "FlujoAprobacion",
                column: "modeloParaleloid");

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobacion_ModeloParalelo_modeloParaleloid",
                table: "FlujoAprobacion",
                column: "modeloParaleloid",
                principalTable: "ModeloParalelo",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloParalelo_Categorias_categoriaid",
                table: "ModeloParalelo",
                column: "categoriaid",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
