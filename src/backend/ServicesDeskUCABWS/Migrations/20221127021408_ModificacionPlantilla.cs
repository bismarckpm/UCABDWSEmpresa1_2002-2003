using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class ModificacionPlantilla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Plantillas_Plantillaid",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_Plantillaid",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "cuerpo",
                table: "Plantillas");

            migrationBuilder.DropColumn(
                name: "Plantillaid",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Plantillas",
                newName: "operacion");

            migrationBuilder.AlterColumn<bool>(
                name: "titulo",
                table: "Plantillas",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Plantillas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "asignadoa",
                table: "Plantillas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "descripcion",
                table: "Plantillas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "fecha",
                table: "Plantillas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Plantillas_TicketId",
                table: "Plantillas",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantillas_Tickets_TicketId",
                table: "Plantillas",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantillas_Tickets_TicketId",
                table: "Plantillas");

            migrationBuilder.DropIndex(
                name: "IX_Plantillas_TicketId",
                table: "Plantillas");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Plantillas");

            migrationBuilder.DropColumn(
                name: "asignadoa",
                table: "Plantillas");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "Plantillas");

            migrationBuilder.DropColumn(
                name: "fecha",
                table: "Plantillas");

            migrationBuilder.RenameColumn(
                name: "operacion",
                table: "Plantillas",
                newName: "tipo");

            migrationBuilder.AlterColumn<string>(
                name: "titulo",
                table: "Plantillas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "cuerpo",
                table: "Plantillas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Plantillaid",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Plantillaid",
                table: "Notifications",
                column: "Plantillaid");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Plantillas_Plantillaid",
                table: "Notifications",
                column: "Plantillaid",
                principalTable: "Plantillas",
                principalColumn: "id");
        }
    }
}
