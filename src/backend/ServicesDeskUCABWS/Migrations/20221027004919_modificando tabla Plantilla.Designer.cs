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
    [Migration("20221027004919_modificando tabla Plantilla")]
    partial class modificandotablaPlantilla
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Cargo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tipoCargoId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("tipoCargoId")
                        .IsUnique();

                    b.ToTable("Cargo");
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

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Notification", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int?>("Plantillaid")
                        .HasColumnType("int");

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("usuarioid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Plantillaid");

                    b.HasIndex("usuarioid");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Plantilla", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("cuerpo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

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

                    b.Property<int>("Statusid")
                        .HasColumnType("int");

                    b.Property<string>("fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("prioridadid")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Statusid");

                    b.HasIndex("prioridadid");

                    b.HasIndex("usuarioid");

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

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Cargo", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Usuario", "user")
                        .WithOne("cargoUser")
                        .HasForeignKey("ServicesDeskUCABWS.Persistence.Entity.Cargo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.TipoCargo", "tipoCargo")
                        .WithOne("cargo_")
                        .HasForeignKey("ServicesDeskUCABWS.Persistence.Entity.Cargo", "tipoCargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tipoCargo");

                    b.Navigation("user");
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

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Notification", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Plantilla", null)
                        .WithMany("notifications")
                        .HasForeignKey("Plantillaid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Ticket", b =>
                {
                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Estado", "Status")
                        .WithMany()
                        .HasForeignKey("Statusid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Prioridad", "prioridad")
                        .WithMany()
                        .HasForeignKey("prioridadid");

                    b.HasOne("ServicesDeskUCABWS.Persistence.Entity.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid");

                    b.Navigation("Status");

                    b.Navigation("prioridad");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Etiqueta", b =>
                {
                    b.Navigation("estados");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Plantilla", b =>
                {
                    b.Navigation("notifications");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.TipoCargo", b =>
                {
                    b.Navigation("cargo_");
                });

            modelBuilder.Entity("ServicesDeskUCABWS.Persistence.Entity.Usuario", b =>
                {
                    b.Navigation("cargoUser");
                });
#pragma warning restore 612, 618
        }
    }
}
