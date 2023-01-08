using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class cambio_tabla_plantilla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipo",
                table: "Plantillas");

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Plantillas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plantillas_EstadoId",
                table: "Plantillas",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantillas_Estados_EstadoId",
                table: "Plantillas",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantillas_Estados_EstadoId",
                table: "Plantillas");

            migrationBuilder.DropIndex(
                name: "IX_Plantillas_EstadoId",
                table: "Plantillas");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Plantillas");

            migrationBuilder.AddColumn<string>(
                name: "tipo",
                table: "Plantillas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
