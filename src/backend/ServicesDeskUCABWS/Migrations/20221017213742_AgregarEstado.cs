using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class AgregarEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Requerimientos_requerimientoid",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Requerimientos");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_requerimientoid",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "requerimientoid",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "Statusid",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Statusid",
                table: "Tickets",
                column: "Statusid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Estado_Statusid",
                table: "Tickets",
                column: "Statusid",
                principalTable: "Estado",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Estado_Statusid",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_Statusid",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Statusid",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "requerimientoid",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Requerimientos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimientos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_requerimientoid",
                table: "Usuario",
                column: "requerimientoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Requerimientos_requerimientoid",
                table: "Usuario",
                column: "requerimientoid",
                principalTable: "Requerimientos",
                principalColumn: "id");
        }
    }
}
