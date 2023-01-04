using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class ModificacionModeloJerarquico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModeloAprobacion_Categorias_categoriaid",
                table: "ModeloAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloAprobacion_Categorias_ModeloParalelo_categoriaid",
                table: "ModeloAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_ModeloAprobacionid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_ModeloJerarquicoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropIndex(
                name: "IX_ModeloJerarquicoCargos_ModeloAprobacionid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropIndex(
                name: "IX_ModeloAprobacion_ModeloParalelo_categoriaid",
                table: "ModeloAprobacion");

            migrationBuilder.DropColumn(
                name: "ModeloAprobacionid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropColumn(
                name: "ModeloParalelo_categoriaid",
                table: "ModeloAprobacion");

            migrationBuilder.RenameColumn(
                name: "ModeloJerarquicoid",
                table: "ModeloJerarquicoCargos",
                newName: "jerarquicoid");

            migrationBuilder.RenameIndex(
                name: "IX_ModeloJerarquicoCargos_ModeloJerarquicoid",
                table: "ModeloJerarquicoCargos",
                newName: "IX_ModeloJerarquicoCargos_jerarquicoid");

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "ModeloAprobacion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "categoriaid",
                table: "ModeloAprobacion",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloAprobacion_Categorias_categoriaid",
                table: "ModeloAprobacion",
                column: "categoriaid",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_jerarquicoid",
                table: "ModeloJerarquicoCargos",
                column: "jerarquicoid",
                principalTable: "ModeloAprobacion",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModeloAprobacion_Categorias_categoriaid",
                table: "ModeloAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_jerarquicoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.RenameColumn(
                name: "jerarquicoid",
                table: "ModeloJerarquicoCargos",
                newName: "ModeloJerarquicoid");

            migrationBuilder.RenameIndex(
                name: "IX_ModeloJerarquicoCargos_jerarquicoid",
                table: "ModeloJerarquicoCargos",
                newName: "IX_ModeloJerarquicoCargos_ModeloJerarquicoid");

            migrationBuilder.AddColumn<int>(
                name: "ModeloAprobacionid",
                table: "ModeloJerarquicoCargos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "ModeloAprobacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "categoriaid",
                table: "ModeloAprobacion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ModeloParalelo_categoriaid",
                table: "ModeloAprobacion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloJerarquicoCargos_ModeloAprobacionid",
                table: "ModeloJerarquicoCargos",
                column: "ModeloAprobacionid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloAprobacion_ModeloParalelo_categoriaid",
                table: "ModeloAprobacion",
                column: "ModeloParalelo_categoriaid");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloAprobacion_Categorias_categoriaid",
                table: "ModeloAprobacion",
                column: "categoriaid",
                principalTable: "Categorias",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloAprobacion_Categorias_ModeloParalelo_categoriaid",
                table: "ModeloAprobacion",
                column: "ModeloParalelo_categoriaid",
                principalTable: "Categorias",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_ModeloAprobacionid",
                table: "ModeloJerarquicoCargos",
                column: "ModeloAprobacionid",
                principalTable: "ModeloAprobacion",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_ModeloJerarquicoid",
                table: "ModeloJerarquicoCargos",
                column: "ModeloJerarquicoid",
                principalTable: "ModeloAprobacion",
                principalColumn: "id");
        }
    }
}
