using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class ModificaciontablaCargos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_TipoCargos_tipocargoid",
                table: "Cargos");

            migrationBuilder.RenameColumn(
                name: "tipocargoid",
                table: "Cargos",
                newName: "tipoCargoId");

            migrationBuilder.RenameIndex(
                name: "IX_Cargos_tipocargoid",
                table: "Cargos",
                newName: "IX_Cargos_tipoCargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_TipoCargos_tipoCargoId",
                table: "Cargos",
                column: "tipoCargoId",
                principalTable: "TipoCargos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_TipoCargos_tipoCargoId",
                table: "Cargos");

            migrationBuilder.RenameColumn(
                name: "tipoCargoId",
                table: "Cargos",
                newName: "tipocargoid");

            migrationBuilder.RenameIndex(
                name: "IX_Cargos_tipoCargoId",
                table: "Cargos",
                newName: "IX_Cargos_tipocargoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_TipoCargos_tipocargoid",
                table: "Cargos",
                column: "tipocargoid",
                principalTable: "TipoCargos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
