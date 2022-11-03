using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class UnionconGrupoB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_TipoCargos_tipoCargoId",
                table: "Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_Usuario_UserId",
                table: "Cargo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargo",
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

            migrationBuilder.RenameTable(
                name: "Cargo",
                newName: "Cargos");

            migrationBuilder.RenameColumn(
                name: "tipoCargoId",
                table: "Cargos",
                newName: "tipocargoid");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "cargoid",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargos",
                table: "Cargos",
                column: "id");

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_cargoid",
                table: "Usuario",
                column: "cargoid");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_tipocargoid",
                table: "Cargos",
                column: "tipocargoid");

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

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_cargoid",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargos",
                table: "Cargos");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_tipocargoid",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "cargoid",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Cargos",
                newName: "Cargo");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo",
                column: "id");

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
