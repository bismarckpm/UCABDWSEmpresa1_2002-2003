using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class TresObjetos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Usuario_usuarioid",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "requerimientoid",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "usuarioid",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Prioridades",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Requerimiento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimiento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCargos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCargos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoCargoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cargo_TipoCargos_tipoCargoId",
                        column: x => x.tipoCargoId,
                        principalTable: "TipoCargos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cargo_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_requerimientoid",
                table: "Usuario",
                column: "requerimientoid");

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
                name: "FK_Notifications_Usuario_usuarioid",
                table: "Notifications",
                column: "usuarioid",
                principalTable: "Usuario",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Requerimiento_requerimientoid",
                table: "Usuario",
                column: "requerimientoid",
                principalTable: "Requerimiento",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Usuario_usuarioid",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Requerimiento_requerimientoid",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Prioridades");

            migrationBuilder.DropTable(
                name: "Requerimiento");

            migrationBuilder.DropTable(
                name: "TipoCargos");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_requerimientoid",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "requerimientoid",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "usuarioid",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Usuario_usuarioid",
                table: "Notifications",
                column: "usuarioid",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
