using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Make_ExchangeAccountId_Nullable_In_Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ExchangeAccounts_ExchangeAccountId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "ExchangeAccountName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ExchangeAccounts_ExchangeAccountId",
                table: "Transactions",
                column: "ExchangeAccountId",
                principalTable: "ExchangeAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ExchangeAccounts_ExchangeAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ExchangeAccountName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletName",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ExchangeAccounts_ExchangeAccountId",
                table: "Transactions",
                column: "ExchangeAccountId",
                principalTable: "ExchangeAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
