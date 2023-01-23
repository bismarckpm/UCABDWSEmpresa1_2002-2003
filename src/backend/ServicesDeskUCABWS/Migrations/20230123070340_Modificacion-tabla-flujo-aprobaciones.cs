using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class Modificaciontablaflujoaprobaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupo_Departamentos_departamentoid",
                table: "Grupo");

            migrationBuilder.RenameColumn(
                name: "estatus",
                table: "FlujoAprobaciones",
                newName: "estatusid");

            migrationBuilder.AlterColumn<int>(
                name: "departamentoid",
                table: "Grupo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estadoid",
                table: "FlujoAprobaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobaciones_Estadoid",
                table: "FlujoAprobaciones",
                column: "Estadoid");

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobaciones_Estados_Estadoid",
                table: "FlujoAprobaciones",
                column: "Estadoid",
                principalTable: "Estados",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupo_Departamentos_departamentoid",
                table: "Grupo",
                column: "departamentoid",
                principalTable: "Departamentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobaciones_Estados_Estadoid",
                table: "FlujoAprobaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupo_Departamentos_departamentoid",
                table: "Grupo");

            migrationBuilder.DropIndex(
                name: "IX_FlujoAprobaciones_Estadoid",
                table: "FlujoAprobaciones");

            migrationBuilder.DropColumn(
                name: "Estadoid",
                table: "FlujoAprobaciones");

            migrationBuilder.RenameColumn(
                name: "estatusid",
                table: "FlujoAprobaciones",
                newName: "estatus");

            migrationBuilder.AlterColumn<int>(
                name: "departamentoid",
                table: "Grupo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupo_Departamentos_departamentoid",
                table: "Grupo",
                column: "departamentoid",
                principalTable: "Departamentos",
                principalColumn: "id");
        }
    }
}
