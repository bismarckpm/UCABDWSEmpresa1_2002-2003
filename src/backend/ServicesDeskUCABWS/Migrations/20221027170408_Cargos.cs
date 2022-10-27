using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class Cargos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_TipoCargos_tipocargoid",
                table: "Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Cargo_cargoid",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo");

            migrationBuilder.RenameTable(
                name: "Cargo",
                newName: "Cargos");

            migrationBuilder.RenameIndex(
                name: "IX_Cargo_tipocargoid",
                table: "Cargos",
                newName: "IX_Cargos_tipocargoid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargos",
                table: "Cargos",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_TipoCargos_tipocargoid",
                table: "Cargos",
                column: "tipocargoid",
                principalTable: "TipoCargos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Cargos_cargoid",
                table: "Usuario",
                column: "cargoid",
                principalTable: "Cargos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_TipoCargos_tipocargoid",
                table: "Cargos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Cargos_cargoid",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargos",
                table: "Cargos");

            migrationBuilder.RenameTable(
                name: "Cargos",
                newName: "Cargo");

            migrationBuilder.RenameIndex(
                name: "IX_Cargos_tipocargoid",
                table: "Cargo",
                newName: "IX_Cargo_tipocargoid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_TipoCargos_tipocargoid",
                table: "Cargo",
                column: "tipocargoid",
                principalTable: "TipoCargos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Cargo_cargoid",
                table: "Usuario",
                column: "cargoid",
                principalTable: "Cargo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
