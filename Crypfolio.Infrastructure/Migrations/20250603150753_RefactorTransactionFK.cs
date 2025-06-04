using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTransactionFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ExchangeAccount_ExchangeAccountId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Wallet_WalletId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_ExchangeAccount_ExchangeAccountId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Wallet_WalletId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId1",
                table: "Wallet");

            migrationBuilder.DropTable(
                name: "ExchangeAccount");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ExchangeAccountId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_WalletId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId1",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "ExchangeAccountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Wallet");

            migrationBuilder.RenameTable(
                name: "Wallet",
                newName: "AccountSource");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountSourceId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountSourceId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AccountSource",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Blockchain",
                table: "AccountSource",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AccountSource",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "AccountSource",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApiKeyEncrypted",
                table: "AccountSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApiPassphrase",
                table: "AccountSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApiSecretEncrypted",
                table: "AccountSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AccountSource",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AccountSource",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "WalletType",
                table: "AccountSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountSource",
                table: "AccountSource",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountSourceId",
                table: "Transaction",
                column: "AccountSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AccountSourceId",
                table: "Assets",
                column: "AccountSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSource_UserId",
                table: "AccountSource",
                column: "UserId");

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

            migrationBuilder.DropIndex(
                name: "IX_Transaction_AccountSourceId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AccountSourceId",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountSource",
                table: "AccountSource");

            migrationBuilder.DropIndex(
                name: "IX_AccountSource_UserId",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "AccountSourceId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "AccountSourceId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "ApiKeyEncrypted",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "ApiPassphrase",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "ApiSecretEncrypted",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "WalletType",
                table: "AccountSource");

            migrationBuilder.RenameTable(
                name: "AccountSource",
                newName: "Wallet");

            migrationBuilder.AddColumn<Guid>(
                name: "ExchangeAccountId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Wallet",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Blockchain",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Wallet",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExchangeAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApiKeyEncrypted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiSecretEncrypted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetrievedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeAccount_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ExchangeAccountId",
                table: "Transaction",
                column: "ExchangeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_WalletId",
                table: "Transaction",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId1",
                table: "Wallet",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeAccount_UserId1",
                table: "ExchangeAccount",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ExchangeAccount_ExchangeAccountId",
                table: "Assets",
                column: "ExchangeAccountId",
                principalTable: "ExchangeAccount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Wallet_WalletId",
                table: "Assets",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_ExchangeAccount_ExchangeAccountId",
                table: "Transaction",
                column: "ExchangeAccountId",
                principalTable: "ExchangeAccount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Wallet_WalletId",
                table: "Transaction",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId1",
                table: "Wallet",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
