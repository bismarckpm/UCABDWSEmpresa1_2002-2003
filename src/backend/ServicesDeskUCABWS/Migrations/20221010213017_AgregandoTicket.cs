using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class AgregandoTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false),
                    usuarioid = table.Column<int>(type: "int", nullable: true),
                    priorId = table.Column<int>(type: "int", nullable: false),
                    prioridadid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tickets_Prioridades_prioridadid",
                        column: x => x.prioridadid,
                        principalTable: "Prioridades",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_prioridadid",
                table: "Tickets",
                column: "prioridadid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_usuarioid",
                table: "Tickets",
                column: "usuarioid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
