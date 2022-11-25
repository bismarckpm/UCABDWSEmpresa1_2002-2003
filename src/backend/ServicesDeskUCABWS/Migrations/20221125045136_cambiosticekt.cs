using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class cambiosticekt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobacion_Tickets_ticketid",
                table: "FlujoAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tickets_delegacionid",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_FlujoAprobacion_ticketid",
                table: "FlujoAprobacion");

            migrationBuilder.RenameColumn(
                name: "delegacionid",
                table: "Tickets",
                newName: "categoriaid");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_delegacionid",
                table: "Tickets",
                newName: "IX_Tickets_categoriaid");

            migrationBuilder.AlterColumn<int>(
                name: "ticketid",
                table: "FlujoAprobacion",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_ticketid",
                table: "FlujoAprobacion",
                column: "ticketid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobacion_Tickets_ticketid",
                table: "FlujoAprobacion",
                column: "ticketid",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Categorias_categoriaid",
                table: "Tickets",
                column: "categoriaid",
                principalTable: "Categorias",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobacion_Tickets_ticketid",
                table: "FlujoAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Categorias_categoriaid",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_FlujoAprobacion_ticketid",
                table: "FlujoAprobacion");

            migrationBuilder.RenameColumn(
                name: "categoriaid",
                table: "Tickets",
                newName: "delegacionid");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_categoriaid",
                table: "Tickets",
                newName: "IX_Tickets_delegacionid");

            migrationBuilder.AlterColumn<int>(
                name: "ticketid",
                table: "FlujoAprobacion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_ticketid",
                table: "FlujoAprobacion",
                column: "ticketid");

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobacion_Tickets_ticketid",
                table: "FlujoAprobacion",
                column: "ticketid",
                principalTable: "Tickets",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tickets_delegacionid",
                table: "Tickets",
                column: "delegacionid",
                principalTable: "Tickets",
                principalColumn: "id");
        }
    }
}
