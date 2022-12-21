using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class fixModeloJerarquicoPart1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_jerarquicoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropIndex(
                name: "IX_ModeloJerarquicoCargos_jerarquicoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropColumn(
                name: "jerarquicoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.RenameColumn(
                name: "modelojerauicoid",
                table: "ModeloJerarquicoCargos",
                newName: "modelojerarquicoid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloJerarquicoCargos_modelojerarquicoid",
                table: "ModeloJerarquicoCargos",
                column: "modelojerarquicoid");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_modelojerarquicoid",
                table: "ModeloJerarquicoCargos",
                column: "modelojerarquicoid",
                principalTable: "ModeloAprobacion",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_modelojerarquicoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.DropIndex(
                name: "IX_ModeloJerarquicoCargos_modelojerarquicoid",
                table: "ModeloJerarquicoCargos");

            migrationBuilder.RenameColumn(
                name: "modelojerarquicoid",
                table: "ModeloJerarquicoCargos",
                newName: "modelojerauicoid");

            migrationBuilder.AddColumn<int>(
                name: "jerarquicoid",
                table: "ModeloJerarquicoCargos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloJerarquicoCargos_jerarquicoid",
                table: "ModeloJerarquicoCargos",
                column: "jerarquicoid");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_jerarquicoid",
                table: "ModeloJerarquicoCargos",
                column: "jerarquicoid",
                principalTable: "ModeloAprobacion",
                principalColumn: "id");
        }
    }
}
