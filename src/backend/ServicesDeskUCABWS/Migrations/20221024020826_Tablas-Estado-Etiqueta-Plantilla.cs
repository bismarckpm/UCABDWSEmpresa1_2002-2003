using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class TablasEstadoEtiquetaPlantilla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Estado_Statusid",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estado",
                table: "Estado");

            migrationBuilder.DropColumn(
                name: "priorId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Estado",
                newName: "Estados");

            migrationBuilder.AddColumn<int>(
                name: "etiquetaid",
                table: "Estados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "notificationid",
                table: "Estados",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estados",
                table: "Estados",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Etiquetas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiquetas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Plantillas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cuerpo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notificationid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantillas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Plantillas_Notifications_notificationid",
                        column: x => x.notificationid,
                        principalTable: "Notifications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estados_etiquetaid",
                table: "Estados",
                column: "etiquetaid");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_notificationid",
                table: "Estados",
                column: "notificationid");

            migrationBuilder.CreateIndex(
                name: "IX_Plantillas_notificationid",
                table: "Plantillas",
                column: "notificationid");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Etiquetas_etiquetaid",
                table: "Estados",
                column: "etiquetaid",
                principalTable: "Etiquetas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Notifications_notificationid",
                table: "Estados",
                column: "notificationid",
                principalTable: "Notifications",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Estados_Statusid",
                table: "Tickets",
                column: "Statusid",
                principalTable: "Estados",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Etiquetas_etiquetaid",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Notifications_notificationid",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Estados_Statusid",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Etiquetas");

            migrationBuilder.DropTable(
                name: "Plantillas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estados",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_etiquetaid",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_notificationid",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "etiquetaid",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "notificationid",
                table: "Estados");

            migrationBuilder.RenameTable(
                name: "Estados",
                newName: "Estado");

            migrationBuilder.AddColumn<int>(
                name: "priorId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estado",
                table: "Estado",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Estado_Statusid",
                table: "Tickets",
                column: "Statusid",
                principalTable: "Estado",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
