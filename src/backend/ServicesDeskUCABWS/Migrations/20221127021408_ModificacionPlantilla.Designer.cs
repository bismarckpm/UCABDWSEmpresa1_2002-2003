﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServicesDeskUCABWS.Persistence.Database;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    [DbContext(typeof(MigrationDbContext))]
    [Migration("20221127021408_ModificacionPlantilla")]
    partial class ModificacionPlantilla
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("notificationid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("EtiquetaId");

                    b.HasIndex("notificationid");

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

                    b.Property<int?>("modeloParaleloparaid")
                        .HasColumnType("int");

                    b.Property<int>("modelojerarquicoid")
                        .HasColumnType("int");

                    b.Property<int>("paraleloid")
                        .HasColumnType("int");

                    b.Property<int>("secuencia")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("ticketid")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("modeloParaleloparaid");

                    b.HasIndex("modelojerarquicoid")
                        .IsUnique();

                    b.HasIndex("ticketid")
                        .IsUnique();

                    b.HasIndex("usuarioid");

                    b.ToTable("FlujoAprobacion");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Grupo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("Departamentoid")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Departamentoid");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("ModeloJerarquicos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloParalelo", b =>
                {
                    b.Property<int>("paraid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("paraid"), 1L, 1);

                    b.Property<int?>("cantidadAprobaciones")
                        .HasColumnType("int");

                    b.Property<int>("categoriaId")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("paraid");

                    b.HasIndex("categoriaId");

                    b.ToTable("ModeloParalelos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Notification", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("usuarioid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("usuarioid");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Plantilla", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<bool>("asignadoa")
                        .HasColumnType("bit");

                    b.Property<bool>("descripcion")
                        .HasColumnType("bit");

                    b.Property<bool>("fecha")
                        .HasColumnType("bit");

                    b.Property<string>("operacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("titulo")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("TicketId");

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

                    b.HasIndex("prioridadid");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.TipoCargo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("ModeloJerarquicoId")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("ModeloJerarquicoId");

                    b.ToTable("TipoCargos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("Departamentoid")
                        .HasColumnType("int");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("passwordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("passwordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("id");

                    b.HasIndex("Departamentoid");

                    b.HasIndex("Grupoid");

                    b.HasIndex("cargoid");

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

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Notification", "notification")
                        .WithMany()
                        .HasForeignKey("notificationid");

                    b.Navigation("etiqueta");

                    b.Navigation("notification");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.FlujoAprobacion", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.ModeloParalelo", "modeloParalelo")
                        .WithMany()
                        .HasForeignKey("modeloParaleloparaid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", "modeloJerarquico")
                        .WithOne("flujoAprobacion")
                        .HasForeignKey("ServicesDeskUCABWS.Persistence.Entity.FlujoAprobacion", "modelojerarquicoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Ticket", "ticket")
                        .WithOne("FlujoAprobacion")
                        .HasForeignKey("ServicesDeskUCABWS.Persistence.Entity.FlujoAprobacion", "ticketid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Usuario", "usuario")
                        .WithMany("Flujo")
                        .HasForeignKey("usuarioid");

                    b.Navigation("modeloJerarquico");

                    b.Navigation("modeloParalelo");

                    b.Navigation("ticket");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Grupo", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Departamento", null)
                        .WithMany("grupos")
                        .HasForeignKey("Departamentoid");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Categoria", "categoria")
                        .WithMany("modelosjerruicos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoria");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloParalelo", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Categoria", "categoria")
                        .WithMany("ModeloParalelos")
                        .HasForeignKey("categoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoria");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Notification", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Plantilla", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
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

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Prioridad", "prioridad")
                        .WithMany()
                        .HasForeignKey("prioridadid");

                    b.Navigation("Estado");

                    b.Navigation("asginadoa");

                    b.Navigation("categoria");

                    b.Navigation("creadopor");

                    b.Navigation("prioridad");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.TipoCargo", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", null)
                        .WithMany("orden")
                        .HasForeignKey("ModeloJerarquicoId");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Usuario", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Departamento", "Departamento")
                        .WithMany("Usuarios")
                        .HasForeignKey("Departamentoid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Grupo", null)
                        .WithMany("usuarios")
                        .HasForeignKey("Grupoid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Cargo", "cargo")
                        .WithMany("Usuarios")
                        .HasForeignKey("cargoid");

                    b.Navigation("Departamento");

                    b.Navigation("cargo");
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
                    b.Navigation("Usuarios");

                    b.Navigation("grupos");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Estado", b =>
                {
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

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.ModeloJerarquico", b =>
                {
                    b.Navigation("flujoAprobacion");

                    b.Navigation("orden");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Ticket", b =>
                {
                    b.Navigation("FlujoAprobacion");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Usuario", b =>
                {
                    b.Navigation("Flujo");

                    b.Navigation("ticketsasignados");

                    b.Navigation("ticketscreados");
                });
#pragma warning restore 612, 618
        }
    }
}
