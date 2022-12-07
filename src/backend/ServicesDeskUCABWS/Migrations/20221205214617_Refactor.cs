using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class Refactor : Migration
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
                name: "ModeloAprobacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoriaid = table.Column<int>(type: "int", nullable: true),
                    cantidaddeaprobacion = table.Column<int>(type: "int", nullable: true),
                    ModeloParalelo_categoriaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloAprobacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_ModeloAprobacion_Categorias_categoriaid",
                        column: x => x.categoriaid,
                        principalTable: "Categorias",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ModeloAprobacion_Categorias_ModeloParalelo_categoriaid",
                        column: x => x.ModeloParalelo_categoriaid,
                        principalTable: "Categorias",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    departamentoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Grupo_Departamentos_departamentoid",
                        column: x => x.departamentoid,
                        principalTable: "Departamentos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EtiquetaId = table.Column<int>(type: "int", nullable: false)
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
                name: "ModeloJerarquicoCargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orden = table.Column<int>(type: "int", nullable: false),
                    modelojerauicoid = table.Column<int>(type: "int", nullable: false),
                    ModeloAprobacionid = table.Column<int>(type: "int", nullable: true),
                    TipoCargoid = table.Column<int>(type: "int", nullable: true),
                    ModeloJerarquicoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloJerarquicoCargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_ModeloAprobacionid",
                        column: x => x.ModeloAprobacionid,
                        principalTable: "ModeloAprobacion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ModeloJerarquicoCargos_ModeloAprobacion_ModeloJerarquicoid",
                        column: x => x.ModeloJerarquicoid,
                        principalTable: "ModeloAprobacion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ModeloJerarquicoCargos_TipoCargos_TipoCargoid",
                        column: x => x.TipoCargoid,
                        principalTable: "TipoCargos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cargoid = table.Column<int>(type: "int", nullable: true),
                    Grupoid = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creadoporid = table.Column<int>(type: "int", nullable: true),
                    asginadoaid = table.Column<int>(type: "int", nullable: true),
                    prioridadid = table.Column<int>(type: "int", nullable: true),
                    grupoid = table.Column<int>(type: "int", nullable: true),
                    categoriaid = table.Column<int>(type: "int", nullable: true),
                    Estadoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tickets_Categorias_categoriaid",
                        column: x => x.categoriaid,
                        principalTable: "Categorias",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Estados_Estadoid",
                        column: x => x.Estadoid,
                        principalTable: "Estados",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Grupo_grupoid",
                        column: x => x.grupoid,
                        principalTable: "Grupo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tickets_Prioridades_prioridadid",
                        column: x => x.prioridadid,
                        principalTable: "Prioridades",
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

            migrationBuilder.CreateTable(
                name: "FlujoAprobaciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketid = table.Column<int>(type: "int", nullable: false),
                    modeloid = table.Column<int>(type: "int", nullable: false),
                    ModeloAprobacionid = table.Column<int>(type: "int", nullable: false),
                    empleadoid = table.Column<int>(type: "int", nullable: false),
                    estatus = table.Column<int>(type: "int", nullable: false),
                    Clienteid = table.Column<int>(type: "int", nullable: true),
                    administradorid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlujoAprobaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_ModeloAprobacion_ModeloAprobacionid",
                        column: x => x.ModeloAprobacionid,
                        principalTable: "ModeloAprobacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_Tickets_ticketid",
                        column: x => x.ticketid,
                        principalTable: "Tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_Usuario_administradorid",
                        column: x => x.administradorid,
                        principalTable: "Usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_Usuario_Clienteid",
                        column: x => x.Clienteid,
                        principalTable: "Usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_Usuario_empleadoid",
                        column: x => x.empleadoid,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TickectsRelacionados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticketid = table.Column<int>(type: "int", nullable: true),
                    TicketRelacionadoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickectsRelacionados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TickectsRelacionados_Tickets_Ticketid",
                        column: x => x.Ticketid,
                        principalTable: "Tickets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TickectsRelacionados_Tickets_TicketRelacionadoid",
                        column: x => x.TicketRelacionadoid,
                        principalTable: "Tickets",
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
                name: "IX_FlujoAprobaciones_administradorid",
                table: "FlujoAprobaciones",
                column: "administradorid");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobaciones_Clienteid",
                table: "FlujoAprobaciones",
                column: "Clienteid");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobaciones_empleadoid",
                table: "FlujoAprobaciones",
                column: "empleadoid");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobaciones_ModeloAprobacionid",
                table: "FlujoAprobaciones",
                column: "ModeloAprobacionid");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobaciones_ticketid",
                table: "FlujoAprobaciones",
                column: "ticketid");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_departamentoid",
                table: "Grupo",
                column: "departamentoid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloAprobacion_categoriaid",
                table: "ModeloAprobacion",
                column: "categoriaid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloAprobacion_ModeloParalelo_categoriaid",
                table: "ModeloAprobacion",
                column: "ModeloParalelo_categoriaid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloJerarquicoCargos_ModeloAprobacionid",
                table: "ModeloJerarquicoCargos",
                column: "ModeloAprobacionid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloJerarquicoCargos_ModeloJerarquicoid",
                table: "ModeloJerarquicoCargos",
                column: "ModeloJerarquicoid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloJerarquicoCargos_TipoCargoid",
                table: "ModeloJerarquicoCargos",
                column: "TipoCargoid");

            migrationBuilder.CreateIndex(
                name: "IX_TickectsRelacionados_Ticketid",
                table: "TickectsRelacionados",
                column: "Ticketid");

            migrationBuilder.CreateIndex(
                name: "IX_TickectsRelacionados_TicketRelacionadoid",
                table: "TickectsRelacionados",
                column: "TicketRelacionadoid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_asginadoaid",
                table: "Tickets",
                column: "asginadoaid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_categoriaid",
                table: "Tickets",
                column: "categoriaid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_creadoporid",
                table: "Tickets",
                column: "creadoporid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Estadoid",
                table: "Tickets",
                column: "Estadoid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_grupoid",
                table: "Tickets",
                column: "grupoid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_prioridadid",
                table: "Tickets",
                column: "prioridadid");

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
                name: "FlujoAprobaciones");

            migrationBuilder.DropTable(
                name: "ModeloJerarquicoCargos");

            migrationBuilder.DropTable(
                name: "Plantillas");

            migrationBuilder.DropTable(
                name: "TickectsRelacionados");

            migrationBuilder.DropTable(
                name: "ModeloAprobacion");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Prioridades");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Etiquetas");

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
