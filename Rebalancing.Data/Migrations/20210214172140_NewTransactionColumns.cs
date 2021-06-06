using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rebalancing.Data.Migrations
{
    public partial class NewTransactionColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SettlementDate",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Transaction",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "SettlementDate",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Transaction");
        }
    }
}
