using Microsoft.EntityFrameworkCore.Migrations;

namespace Rebalancing.Data.Migrations
{
    public partial class ChangeSymbolColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Position_Security_SecurityId",
                table: "Position");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Security_SecurityId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_SecurityId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Position_SecurityId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "SecurityId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "SecurityId",
                table: "Position");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Position",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Position");

            migrationBuilder.AddColumn<int>(
                name: "SecurityId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecurityId",
                table: "Position",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_SecurityId",
                table: "Transaction",
                column: "SecurityId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_SecurityId",
                table: "Position",
                column: "SecurityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Security_SecurityId",
                table: "Position",
                column: "SecurityId",
                principalTable: "Security",
                principalColumn: "SecurityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Security_SecurityId",
                table: "Transaction",
                column: "SecurityId",
                principalTable: "Security",
                principalColumn: "SecurityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
