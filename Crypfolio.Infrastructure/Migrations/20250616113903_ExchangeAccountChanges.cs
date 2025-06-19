using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExchangeAccountChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSources_AspNetUsers_UserId",
                table: "AccountSources");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AccountSources_AccountSourceId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AccountSources_ExchangeAccountId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AccountSources_WalletId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AccountSources_AccountSourceId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountSources",
                table: "AccountSources");

            migrationBuilder.RenameTable(
                name: "AccountSources",
                newName: "AccountSource");

            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "Assets",
                newName: "Ticker");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AccountSource",
                newName: "AccountName");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSources_UserId",
                table: "AccountSource",
                newName: "IX_AccountSource_UserId");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeName",
                table: "AccountSource",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountSource",
                table: "AccountSource",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSource_AspNetUsers_UserId",
                table: "AccountSource",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AccountSource_AccountSourceId",
                table: "Assets",
                column: "AccountSourceId",
                principalTable: "AccountSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AccountSource_ExchangeAccountId",
                table: "Assets",
                column: "ExchangeAccountId",
                principalTable: "AccountSource",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AccountSource_WalletId",
                table: "Assets",
                column: "WalletId",
                principalTable: "AccountSource",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AccountSource_AccountSourceId",
                table: "Transaction",
                column: "AccountSourceId",
                principalTable: "AccountSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSource_AspNetUsers_UserId",
                table: "AccountSource");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AccountSource_AccountSourceId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AccountSource_ExchangeAccountId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AccountSource_WalletId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AccountSource_AccountSourceId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountSource",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "ExchangeName",
                table: "AccountSource");

            migrationBuilder.RenameTable(
                name: "AccountSource",
                newName: "AccountSources");

            migrationBuilder.RenameColumn(
                name: "Ticker",
                table: "Assets",
                newName: "Symbol");

            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "AccountSources",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSource_UserId",
                table: "AccountSources",
                newName: "IX_AccountSources_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountSources",
                table: "AccountSources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSources_AspNetUsers_UserId",
                table: "AccountSources",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AccountSources_AccountSourceId",
                table: "Assets",
                column: "AccountSourceId",
                principalTable: "AccountSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AccountSources_ExchangeAccountId",
                table: "Assets",
                column: "ExchangeAccountId",
                principalTable: "AccountSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AccountSources_WalletId",
                table: "Assets",
                column: "WalletId",
                principalTable: "AccountSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AccountSources_AccountSourceId",
                table: "Transaction",
                column: "AccountSourceId",
                principalTable: "AccountSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
