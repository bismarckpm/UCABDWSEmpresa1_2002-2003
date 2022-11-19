using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class DepartamentoGrupo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grupoid",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departamentoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Grupos_Departamentos_Departamentoid",
                        column: x => x.Departamentoid,
                        principalTable: "Departamentos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Grupoid",
                table: "Usuario",
                column: "Grupoid");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_Departamentoid",
                table: "Grupos",
                column: "Departamentoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Grupos_Grupoid",
                table: "Usuario",
                column: "Grupoid",
                principalTable: "Grupos",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Grupos_Grupoid",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Grupoid",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Grupoid",
                table: "Usuario");
        }
    }
}
