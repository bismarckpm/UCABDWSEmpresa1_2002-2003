using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class ModificandoatributoentablaEstados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Etiquetas_etiquetaid",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "idEtiqueta",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "etiquetaid",
                table: "Estados",
                newName: "EtiquetaId");

            migrationBuilder.RenameIndex(
                name: "IX_Estados_etiquetaid",
                table: "Estados",
                newName: "IX_Estados_EtiquetaId");

            migrationBuilder.AlterColumn<int>(
                name: "EtiquetaId",
                table: "Estados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Etiquetas_EtiquetaId",
                table: "Estados",
                column: "EtiquetaId",
                principalTable: "Etiquetas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Etiquetas_EtiquetaId",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "EtiquetaId",
                table: "Estados",
                newName: "etiquetaid");

            migrationBuilder.RenameIndex(
                name: "IX_Estados_EtiquetaId",
                table: "Estados",
                newName: "IX_Estados_etiquetaid");

            migrationBuilder.AlterColumn<int>(
                name: "etiquetaid",
                table: "Estados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "idEtiqueta",
                table: "Estados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Etiquetas_etiquetaid",
                table: "Estados",
                column: "etiquetaid",
                principalTable: "Etiquetas",
                principalColumn: "id");
        }
    }
}
