using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class FlujoAprobacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Notifications_notificationid",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobacion_ModeloJerarquicos_modelojerarquicoid",
                table: "FlujoAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobacion_ModeloParalelos_modeloParaleloparaid",
                table: "FlujoAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobacion_Tickets_ticketid",
                table: "FlujoAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobacion_Usuario_usuarioid",
                table: "FlujoAprobacion");

            migrationBuilder.DropForeignKey(
                name: "FK_grupos_Departamentos_Departamentoid",
                table: "grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_grupos_Grupoid",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Grupoid",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Estados_notificationid",
                table: "Estados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_grupos",
                table: "grupos");

            migrationBuilder.DropIndex(
                name: "IX_grupos_Departamentoid",
                table: "grupos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlujoAprobacion",
                table: "FlujoAprobacion");

            migrationBuilder.DropColumn(
                name: "Grupoid",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "notificationid",
                table: "Estados");

            migrationBuilder.RenameTable(
                name: "grupos",
                newName: "Grupo");

            migrationBuilder.RenameTable(
                name: "FlujoAprobacion",
                newName: "FlujoAprobaciones");

            migrationBuilder.RenameColumn(
                name: "Departamentoid",
                table: "Grupo",
                newName: "departamentoid");

            migrationBuilder.RenameIndex(
                name: "IX_FlujoAprobacion_usuarioid",
                table: "FlujoAprobaciones",
                newName: "IX_FlujoAprobaciones_usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_FlujoAprobacion_ticketid",
                table: "FlujoAprobaciones",
                newName: "IX_FlujoAprobaciones_ticketid");

            migrationBuilder.RenameIndex(
                name: "IX_FlujoAprobacion_modeloParaleloparaid",
                table: "FlujoAprobaciones",
                newName: "IX_FlujoAprobaciones_modeloParaleloparaid");

            migrationBuilder.RenameIndex(
                name: "IX_FlujoAprobacion_modelojerarquicoid",
                table: "FlujoAprobaciones",
                newName: "IX_FlujoAprobaciones_modelojerarquicoid");

            migrationBuilder.AlterColumn<int>(
                name: "departamentoid",
                table: "Grupo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grupo",
                table: "Grupo",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlujoAprobaciones",
                table: "FlujoAprobaciones",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobaciones_ModeloJerarquicos_modelojerarquicoid",
                table: "FlujoAprobaciones",
                column: "modelojerarquicoid",
                principalTable: "ModeloJerarquicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobaciones_ModeloParalelos_modeloParaleloparaid",
                table: "FlujoAprobaciones",
                column: "modeloParaleloparaid",
                principalTable: "ModeloParalelos",
                principalColumn: "paraid");

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobaciones_Tickets_ticketid",
                table: "FlujoAprobaciones",
                column: "ticketid",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobaciones_Usuario_usuarioid",
                table: "FlujoAprobaciones",
                column: "usuarioid",
                principalTable: "Usuario",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobaciones_ModeloJerarquicos_modelojerarquicoid",
                table: "FlujoAprobaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobaciones_ModeloParalelos_modeloParaleloparaid",
                table: "FlujoAprobaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobaciones_Tickets_ticketid",
                table: "FlujoAprobaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_FlujoAprobaciones_Usuario_usuarioid",
                table: "FlujoAprobaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grupo",
                table: "Grupo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlujoAprobaciones",
                table: "FlujoAprobaciones");

            migrationBuilder.RenameTable(
                name: "Grupo",
                newName: "grupos");

            migrationBuilder.RenameTable(
                name: "FlujoAprobaciones",
                newName: "FlujoAprobacion");

            migrationBuilder.RenameColumn(
                name: "departamentoid",
                table: "grupos",
                newName: "Departamentoid");

            migrationBuilder.RenameIndex(
                name: "IX_FlujoAprobaciones_usuarioid",
                table: "FlujoAprobacion",
                newName: "IX_FlujoAprobacion_usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_FlujoAprobaciones_ticketid",
                table: "FlujoAprobacion",
                newName: "IX_FlujoAprobacion_ticketid");

            migrationBuilder.RenameIndex(
                name: "IX_FlujoAprobaciones_modeloParaleloparaid",
                table: "FlujoAprobacion",
                newName: "IX_FlujoAprobacion_modeloParaleloparaid");

            migrationBuilder.RenameIndex(
                name: "IX_FlujoAprobaciones_modelojerarquicoid",
                table: "FlujoAprobacion",
                newName: "IX_FlujoAprobacion_modelojerarquicoid");

            migrationBuilder.AddColumn<int>(
                name: "Grupoid",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "notificationid",
                table: "Estados",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Departamentoid",
                table: "grupos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_grupos",
                table: "grupos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlujoAprobacion",
                table: "FlujoAprobacion",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuarioid = table.Column<int>(type: "int", nullable: true),
                    Plantillaid = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notifications_Plantillas_Plantillaid",
                        column: x => x.Plantillaid,
                        principalTable: "Plantillas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Notifications_Usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Grupoid",
                table: "Usuario",
                column: "Grupoid");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_notificationid",
                table: "Estados",
                column: "notificationid");

            migrationBuilder.CreateIndex(
                name: "IX_grupos_Departamentoid",
                table: "grupos",
                column: "Departamentoid");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Plantillaid",
                table: "Notifications",
                column: "Plantillaid");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_usuarioid",
                table: "Notifications",
                column: "usuarioid");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Notifications_notificationid",
                table: "Estados",
                column: "notificationid",
                principalTable: "Notifications",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobacion_ModeloJerarquicos_modelojerarquicoid",
                table: "FlujoAprobacion",
                column: "modelojerarquicoid",
                principalTable: "ModeloJerarquicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobacion_ModeloParalelos_modeloParaleloparaid",
                table: "FlujoAprobacion",
                column: "modeloParaleloparaid",
                principalTable: "ModeloParalelos",
                principalColumn: "paraid");

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobacion_Tickets_ticketid",
                table: "FlujoAprobacion",
                column: "ticketid",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlujoAprobacion_Usuario_usuarioid",
                table: "FlujoAprobacion",
                column: "usuarioid",
                principalTable: "Usuario",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_grupos_Departamentos_Departamentoid",
                table: "grupos",
                column: "Departamentoid",
                principalTable: "Departamentos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_grupos_Grupoid",
                table: "Usuario",
                column: "Grupoid",
                principalTable: "grupos",
                principalColumn: "id");
        }
    }
}
