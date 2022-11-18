using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class modificandotablaPlantilla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantillas_Notifications_notificationid",
                table: "Plantillas");

            migrationBuilder.DropIndex(
                name: "IX_Plantillas_notificationid",
                table: "Plantillas");

            migrationBuilder.DropColumn(
                name: "fecha",
                table: "Plantillas");

            migrationBuilder.DropColumn(
                name: "notificationid",
                table: "Plantillas");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Plantillas_Plantillaid",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_Plantillaid",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Plantillaid",
                table: "Notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha",
                table: "Plantillas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "notificationid",
                table: "Plantillas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plantillas_notificationid",
                table: "Plantillas",
                column: "notificationid");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantillas_Notifications_notificationid",
                table: "Plantillas",
                column: "notificationid",
                principalTable: "Notifications",
                principalColumn: "id");
        }
    }
}
