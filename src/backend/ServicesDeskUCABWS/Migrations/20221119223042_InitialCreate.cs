using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });

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
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantillas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Prioridades",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCargos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCargos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departamentoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Grupo_Departamentos_Departamentoid",
                        column: x => x.Departamentoid,
                        principalTable: "Departamentos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoCargoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cargos_TipoCargos_tipoCargoId",
                        column: x => x.tipoCargoId,
                        principalTable: "TipoCargos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cargoid = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grupoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuario_Cargos_cargoid",
                        column: x => x.cargoid,
                        principalTable: "Cargos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Usuario_Grupo_Grupoid",
                        column: x => x.Grupoid,
                        principalTable: "Grupo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usuarioid = table.Column<int>(type: "int", nullable: true),
                    Plantillaid = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EtiquetaId = table.Column<int>(type: "int", nullable: false),
                    notificationid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.id);
                    table.ForeignKey(
                        name: "FK_Estados_Etiquetas_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "Etiquetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estados_Notifications_notificationid",
                        column: x => x.notificationid,
                        principalTable: "Notifications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creadoporid = table.Column<int>(type: "int", nullable: true),
                    asginadoaid = table.Column<int>(type: "int", nullable: true),
                    prioridadid = table.Column<int>(type: "int", nullable: true),
                    delegacionid = table.Column<int>(type: "int", nullable: true),
                    Statusid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tickets_Estados_Statusid",
                        column: x => x.Statusid,
                        principalTable: "Estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Prioridades_prioridadid",
                        column: x => x.prioridadid,
                        principalTable: "Prioridades",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Tickets_delegacionid",
                        column: x => x.delegacionid,
                        principalTable: "Tickets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Usuario_asginadoaid",
                        column: x => x.asginadoaid,
                        principalTable: "Usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Usuario_creadoporid",
                        column: x => x.creadoporid,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_tipoCargoId",
                table: "Cargos",
                column: "tipoCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_EtiquetaId",
                table: "Estados",
                column: "EtiquetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_notificationid",
                table: "Estados",
                column: "notificationid");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_Departamentoid",
                table: "Grupo",
                column: "Departamentoid");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Plantillaid",
                table: "Notifications",
                column: "Plantillaid");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_usuarioid",
                table: "Notifications",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_asginadoaid",
                table: "Tickets",
                column: "asginadoaid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_creadoporid",
                table: "Tickets",
                column: "creadoporid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_delegacionid",
                table: "Tickets",
                column: "delegacionid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_prioridadid",
                table: "Tickets",
                column: "prioridadid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Statusid",
                table: "Tickets",
                column: "Statusid");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_cargoid",
                table: "Usuario",
                column: "cargoid");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Grupoid",
                table: "Usuario",
                column: "Grupoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Prioridades");

            migrationBuilder.DropTable(
                name: "Etiquetas");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Plantillas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "TipoCargos");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
