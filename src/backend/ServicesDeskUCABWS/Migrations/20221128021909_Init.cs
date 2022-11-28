using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class Init : Migration
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
                name: "Grupo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    departamentoid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.id);
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
                name: "ModeloJerarquicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloJerarquicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloJerarquicos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloParalelos",
                columns: table => new
                {
                    paraid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cantidadAprobaciones = table.Column<int>(type: "int", nullable: true),
                    categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloParalelos", x => x.paraid);
                    table.ForeignKey(
                        name: "FK_ModeloParalelos_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "TipoCargos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeloJerarquicoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCargos", x => x.id);
                    table.ForeignKey(
                        name: "FK_TipoCargos_ModeloJerarquicos_ModeloJerarquicoId",
                        column: x => x.ModeloJerarquicoId,
                        principalTable: "ModeloJerarquicos",
                        principalColumn: "Id");
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
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cargoid = table.Column<int>(type: "int", nullable: true),
                    Departamentoid = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Usuario_Departamentos_Departamentoid",
                        column: x => x.Departamentoid,
                        principalTable: "Departamentos",
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
                    modelojerarquicoid = table.Column<int>(type: "int", nullable: false),
                    modeloParaleloparaid = table.Column<int>(type: "int", nullable: true),
                    paraleloid = table.Column<int>(type: "int", nullable: false),
                    usuarioid = table.Column<int>(type: "int", nullable: true),
                    secuencia = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlujoAprobaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_ModeloJerarquicos_modelojerarquicoid",
                        column: x => x.modelojerarquicoid,
                        principalTable: "ModeloJerarquicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_ModeloParalelos_modeloParaleloparaid",
                        column: x => x.modeloParaleloparaid,
                        principalTable: "ModeloParalelos",
                        principalColumn: "paraid");
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_Tickets_ticketid",
                        column: x => x.ticketid,
                        principalTable: "Tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlujoAprobaciones_Usuario_usuarioid",
                        column: x => x.usuarioid,
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
                name: "IX_FlujoAprobaciones_modelojerarquicoid",
                table: "FlujoAprobaciones",
                column: "modelojerarquicoid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobaciones_modeloParaleloparaid",
                table: "FlujoAprobaciones",
                column: "modeloParaleloparaid");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobaciones_ticketid",
                table: "FlujoAprobaciones",
                column: "ticketid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobaciones_usuarioid",
                table: "FlujoAprobaciones",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloJerarquicos_CategoriaId",
                table: "ModeloJerarquicos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloParalelos_categoriaId",
                table: "ModeloParalelos",
                column: "categoriaId");

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
                name: "IX_Tickets_prioridadid",
                table: "Tickets",
                column: "prioridadid");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCargos_ModeloJerarquicoId",
                table: "TipoCargos",
                column: "ModeloJerarquicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_cargoid",
                table: "Usuario",
                column: "cargoid");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Departamentoid",
                table: "Usuario",
                column: "Departamentoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlujoAprobaciones");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Plantillas");

            migrationBuilder.DropTable(
                name: "ModeloParalelos");

            migrationBuilder.DropTable(
                name: "Tickets");

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
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "TipoCargos");

            migrationBuilder.DropTable(
                name: "ModeloJerarquicos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
