using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class AgregarRequerimientos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Requerimiento_requerimientoid",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requerimiento",
                table: "Requerimiento");

            migrationBuilder.RenameTable(
                name: "Requerimiento",
                newName: "Requerimientos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requerimientos",
                table: "Requerimientos",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Requerimientos_requerimientoid",
                table: "Usuario",
                column: "requerimientoid",
                principalTable: "Requerimientos",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Requerimientos_requerimientoid",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requerimientos",
                table: "Requerimientos");

            migrationBuilder.RenameTable(
                name: "Requerimientos",
                newName: "Requerimiento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requerimiento",
                table: "Requerimiento",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Requerimiento_requerimientoid",
                table: "Usuario",
                column: "requerimientoid",
                principalTable: "Requerimiento",
                principalColumn: "id");
        }
    }
}
