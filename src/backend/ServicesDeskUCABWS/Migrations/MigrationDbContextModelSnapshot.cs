﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServicesDeskUCABWS.Persistence.Database;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    [DbContext(typeof(MigrationDbContext))]
    partial class MigrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Cargo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tipoCargoId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("tipoCargoId");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Categoria", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Departamento", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Estado", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("EtiquetaId")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("EtiquetaId");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Etiqueta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Etiquetas");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.FlujoAprobacion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("Clienteid")
                        .HasColumnType("int");

                    b.Property<int>("ModeloAprobacionid")
                        .HasColumnType("int");

                    b.Property<int?>("administradorid")
                        .HasColumnType("int");

                    b.Property<int>("empleadoid")
                        .HasColumnType("int");

                    b.Property<int>("estatus")
                        .HasColumnType("int");

                    b.Property<int>("modeloid")
                        .HasColumnType("int");

                    b.Property<int>("ticketid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Clienteid");

                    b.HasIndex("ModeloAprobacionid");

                    b.HasIndex("administradorid");

                    b.HasIndex("empleadoid");

                    b.HasIndex("ticketid")
                        .IsUnique();

                    b.ToTable("FlujoAprobaciones");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Grupo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("departamentoid")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("departamentoid");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloAprobacion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("categoriaid")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("ModeloAprobacion");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ModeloAprobacion");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquicoCargos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("TipoCargoid")
                        .HasColumnType("int");

                    b.Property<int>("modelojerarquicoid")
                        .HasColumnType("int");

                    b.Property<int>("orden")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoCargoid");

                    b.HasIndex("modelojerarquicoid");

                    b.ToTable("ModeloJerarquicoCargos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Plantilla", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<string>("cuerpo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Plantillas");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Prioridad", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Prioridades");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.TickectsRelacionados", b =>
                {
                    b.Property<int?>("Ticketid")
                        .HasColumnType("int");

                    b.Property<int?>("TicketRelacionadoid")
                        .HasColumnType("int");

                    b.HasKey("Ticketid", "TicketRelacionadoid");

                    b.HasIndex("TicketRelacionadoid");

                    b.ToTable("TickectsRelacionados");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Ticket", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("Estadoid")
                        .HasColumnType("int");

                    b.Property<int?>("asginadoaid")
                        .HasColumnType("int");

                    b.Property<int?>("categoriaid")
                        .HasColumnType("int");

                    b.Property<int?>("creadoporid")
                        .HasColumnType("int");

                    b.Property<int?>("departamentoid")
                        .HasColumnType("int");

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("prioridadid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Estadoid");

                    b.HasIndex("asginadoaid");

                    b.HasIndex("categoriaid");

                    b.HasIndex("creadoporid");

                    b.HasIndex("departamentoid");

                    b.HasIndex("prioridadid");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.TipoCargo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("TipoCargos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Grupoid")
                        .HasColumnType("int");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("cargoid")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("passwordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("passwordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("id");

                    b.HasIndex("Grupoid");

                    b.HasIndex("cargoid");

                    b.HasIndex("email")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.ToTable("Usuario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.administrador", b =>
                {
                    b.HasBaseType("ServicesDeskUCABWS.Persistence.Entity.Usuario");

                    b.HasDiscriminator().HasValue("administrador");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Cliente", b =>
                {
                    b.HasBaseType("ServicesDeskUCABWS.Persistence.Entity.Usuario");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Empleado", b =>
                {
                    b.HasBaseType("ServicesDeskUCABWS.Persistence.Entity.Usuario");

                    b.HasDiscriminator().HasValue("Empleado");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", b =>
                {
                    b.HasBaseType("ServicesDeskUCABWS.Persistence.Entity.ModeloAprobacion");

                    b.HasIndex("categoriaid");

                    b.HasDiscriminator().HasValue("ModeloJerarquico");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloParalelo", b =>
                {
                    b.HasBaseType("ServicesDeskUCABWS.Persistence.Entity.ModeloAprobacion");

                    b.Property<int>("cantidaddeaprobacion")
                        .HasColumnType("int");

                    b.HasIndex("categoriaid");

                    b.HasDiscriminator().HasValue("ModeloParalelo");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Cargo", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.TipoCargo", "tipoCargo")
                        .WithMany()
                        .HasForeignKey("tipoCargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tipoCargo");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Estado", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Etiqueta", "etiqueta")
                        .WithMany("estados")
                        .HasForeignKey("EtiquetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("etiqueta");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.FlujoAprobacion", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Cliente", null)
                        .WithMany("Flujo")
                        .HasForeignKey("Clienteid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.ModeloAprobacion", "ModeloAprobacion")
                        .WithMany()
                        .HasForeignKey("ModeloAprobacionid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.administrador", null)
                        .WithMany("Flujo")
                        .HasForeignKey("administradorid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Empleado", "Empleado")
                        .WithMany("Flujo")
                        .HasForeignKey("empleadoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Ticket", "Ticket")
                        .WithOne("FlujoAprobacion")
                        .HasForeignKey("ServicesDeskUCABWS.Persistence.Entity.FlujoAprobacion", "ticketid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("ModeloAprobacion");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Grupo", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Departamento", "departamento")
                        .WithMany("Grupos")
                        .HasForeignKey("departamentoid");

                    b.Navigation("departamento");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquicoCargos", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.TipoCargo", "TipoCargo")
                        .WithMany("Jeraruia")
                        .HasForeignKey("TipoCargoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", "jerarquico")
                        .WithMany("Jeraruia")
                        .HasForeignKey("modelojerarquicoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoCargo");

                    b.Navigation("jerarquico");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Plantilla", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Estado", "estado")
                        .WithMany("plantillas")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("estado");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.TickectsRelacionados", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Ticket", "TicketRelacion")
                        .WithMany("TickectsRelacionadosHijos")
                        .HasForeignKey("TicketRelacionadoid")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Ticket", "ticket")
                        .WithMany("TickectsRelacionadosPadre")
                        .HasForeignKey("Ticketid")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("TicketRelacion");

                    b.Navigation("ticket");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Ticket", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Estado", "Estado")
                        .WithMany("tickets")
                        .HasForeignKey("Estadoid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Usuario", "asginadoa")
                        .WithMany("ticketsasignados")
                        .HasForeignKey("asginadoaid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("categoriaid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Usuario", "creadopor")
                        .WithMany("ticketscreados")
                        .HasForeignKey("creadoporid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Departamento", "departamento")
                        .WithMany("Tickets")
                        .HasForeignKey("departamentoid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Prioridad", "prioridad")
                        .WithMany()
                        .HasForeignKey("prioridadid");

                    b.Navigation("Estado");

                    b.Navigation("asginadoa");

                    b.Navigation("categoria");

                    b.Navigation("creadopor");

                    b.Navigation("departamento");

                    b.Navigation("prioridad");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Usuario", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Grupo", "Grupo")
                        .WithMany("usuarios")
                        .HasForeignKey("Grupoid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Cargo", "cargo")
                        .WithMany("Usuarios")
                        .HasForeignKey("cargoid");

                    b.Navigation("Grupo");

                    b.Navigation("cargo");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Categoria", "categoria")
                        .WithMany("modelosjerruicos")
                        .HasForeignKey("categoriaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoria");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloParalelo", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Categoria", "categoria")
                        .WithMany("ModeloParalelos")
                        .HasForeignKey("categoriaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoria");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Cargo", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Categoria", b =>
                {
                    b.Navigation("ModeloParalelos");

                    b.Navigation("modelosjerruicos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Departamento", b =>
                {
                    b.Navigation("Grupos");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Estado", b =>
                {
                    b.Navigation("plantillas");

                    b.Navigation("tickets");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Etiqueta", b =>
                {
                    b.Navigation("estados");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Grupo", b =>
                {
                    b.Navigation("usuarios");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Ticket", b =>
                {
                    b.Navigation("FlujoAprobacion");

                    b.Navigation("TickectsRelacionadosHijos");

                    b.Navigation("TickectsRelacionadosPadre");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.TipoCargo", b =>
                {
                    b.Navigation("Jeraruia");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Usuario", b =>
                {
                    b.Navigation("ticketsasignados");

                    b.Navigation("ticketscreados");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.administrador", b =>
                {
                    b.Navigation("Flujo");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Cliente", b =>
                {
                    b.Navigation("Flujo");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Empleado", b =>
                {
                    b.Navigation("Flujo");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", b =>
                {
                    b.Navigation("Jeraruia");
                });
#pragma warning restore 612, 618
        }
    }
}
