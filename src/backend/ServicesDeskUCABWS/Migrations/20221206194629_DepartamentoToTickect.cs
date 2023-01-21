using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class DepartamentoToTickect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Grupo_grupoid",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "grupoid",
                table: "Tickets",
                newName: "departamentoid");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_grupoid",
                table: "Tickets",
                newName: "IX_Tickets_departamentoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Departamentos_departamentoid",
                table: "Tickets",
                column: "departamentoid",
                principalTable: "Departamentos",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Departamentos_departamentoid",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "departamentoid",
                table: "Tickets",
                newName: "grupoid");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_departamentoid",
                table: "Tickets",
                newName: "IX_Tickets_grupoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Grupo_grupoid",
                table: "Tickets",
                column: "grupoid",
                principalTable: "Grupo",
                principalColumn: "id");
        }
    }
}
