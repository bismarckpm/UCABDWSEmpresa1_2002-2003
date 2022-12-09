using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicesDeskUCABWS.Migrations
{
    public partial class CompositeRelationshipMerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TickectsRelacionados",
                table: "TickectsRelacionados");

            migrationBuilder.DropIndex(
                name: "IX_TickectsRelacionados_Ticketid",
                table: "TickectsRelacionados");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TickectsRelacionados");

            migrationBuilder.AlterColumn<int>(
                name: "Ticketid",
                table: "TickectsRelacionados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TicketRelacionadoid",
                table: "TickectsRelacionados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickectsRelacionados",
                table: "TickectsRelacionados",
                columns: new[] { "Ticketid", "TicketRelacionadoid" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TickectsRelacionados",
                table: "TickectsRelacionados");

            migrationBuilder.AlterColumn<int>(
                name: "TicketRelacionadoid",
                table: "TickectsRelacionados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Ticketid",
                table: "TickectsRelacionados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TickectsRelacionados",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickectsRelacionados",
                table: "TickectsRelacionados",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TickectsRelacionados_Ticketid",
                table: "TickectsRelacionados",
                column: "Ticketid");
        }
    }
}
