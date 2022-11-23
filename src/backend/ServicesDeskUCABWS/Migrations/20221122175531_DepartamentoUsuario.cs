using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class DepartamentoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.AddColumn<int>(
                name: "Departamentoid",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Departamentoid",
                table: "Usuario",
                column: "Departamentoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Departamentos_Departamentoid",
                table: "Usuario",
                column: "Departamentoid",
                principalTable: "Departamentos",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Departamentos_Departamentoid",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Departamentoid",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Departamentoid",
                table: "Usuario");

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });
        }
    }
}
