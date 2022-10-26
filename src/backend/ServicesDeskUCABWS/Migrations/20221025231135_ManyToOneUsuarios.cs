using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class ManyToOneUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_TipoCargos_tipoCargoId",
                table: "Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_Usuario_UserId",
                table: "Cargo");

            migrationBuilder.DropIndex(
                name: "IX_Cargo_tipoCargoId",
                table: "Cargo");

            migrationBuilder.DropIndex(
                name: "IX_Cargo_UserId",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cargo");

            migrationBuilder.RenameColumn(
                name: "tipoCargoId",
                table: "Cargo",
                newName: "tipocargoid");

            migrationBuilder.AddColumn<int>(
                name: "cargoid",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_cargoid",
                table: "Usuario",
                column: "cargoid");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_tipocargoid",
                table: "Cargo",
                column: "tipocargoid");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_TipoCargos_tipocargoid",
                table: "Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Cargo_cargoid",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_cargoid",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Cargo_tipocargoid",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "cargoid",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "tipocargoid",
                table: "Cargo",
                newName: "tipoCargoId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cargo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_tipoCargoId",
                table: "Cargo",
                column: "tipoCargoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_UserId",
                table: "Cargo",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_TipoCargos_tipoCargoId",
                table: "Cargo",
                column: "tipoCargoId",
                principalTable: "TipoCargos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_Usuario_UserId",
                table: "Cargo",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
