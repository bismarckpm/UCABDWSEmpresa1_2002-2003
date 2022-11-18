using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class changestoPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Usuario",
                newName: "VerificationToken");

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "Usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "Usuario",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "Usuario",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "VerificationToken",
                table: "Usuario",
                newName: "password");
        }
    }
}
