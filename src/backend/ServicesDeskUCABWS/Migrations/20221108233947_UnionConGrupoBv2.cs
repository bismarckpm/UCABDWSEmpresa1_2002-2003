using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class UnionConGrupoBv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Cargos_cargoid",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "cargoid",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Cargos_cargoid",
                table: "Usuario",
                column: "cargoid",
                principalTable: "Cargos",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Cargos_cargoid",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "cargoid",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Cargos_cargoid",
                table: "Usuario",
                column: "cargoid",
                principalTable: "Cargos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
