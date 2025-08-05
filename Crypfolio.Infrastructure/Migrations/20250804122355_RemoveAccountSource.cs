using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAccountSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Transactions_AccountSource_AccountSourceId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_AssetId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountSourceId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AssetId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountSource",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "AccountSourceId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Transactions");

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
                name: "Blockchain",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "ExchangeName",
                table: "AccountSource");

            migrationBuilder.RenameTable(
                name: "AccountSource",
                newName: "Wallets");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "ToAmount");

            migrationBuilder.RenameColumn(
                name: "RetrievedAt",
                table: "Assets",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Assets",
                newName: "LockedBalance");

            migrationBuilder.RenameColumn(
                name: "AccountSourceId",
                table: "Assets",
                newName: "WalletId1");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_AccountSourceId",
                table: "Assets",
                newName: "IX_Assets_WalletId1");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSource_UserId",
                table: "Wallets",
                newName: "IX_Wallets_UserId");

            migrationBuilder.AddColumn<string>(
                name: "BlockchainTxHash",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExchangeAccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExchangeOrderId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "Transactions",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "FeeAssetId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromAddress",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FromAmount",
                table: "Transactions",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "FromAssetId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Transactions",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToAddress",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ToAssetId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId1",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExchangeAccountId1",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FreeBalance",
                table: "Assets",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "WalletType",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExchangeAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeName = table.Column<int>(type: "int", nullable: false),
                    ApiKeyEncrypted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiSecretEncrypted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiPassphrase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ExchangeAccountId",
                table: "Transactions",
                column: "ExchangeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FeeAssetId",
                table: "Transactions",
                column: "FeeAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromAssetId",
                table: "Transactions",
                column: "FromAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToAssetId",
                table: "Transactions",
                column: "ToAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId1",
                table: "Transactions",
                column: "WalletId1");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ExchangeAccountId1",
                table: "Assets",
                column: "ExchangeAccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeAccounts_UserId",
                table: "ExchangeAccounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ExchangeAccounts_ExchangeAccountId",
                table: "Assets",
                column: "ExchangeAccountId",
                principalTable: "ExchangeAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ExchangeAccounts_ExchangeAccountId1",
                table: "Assets",
                column: "ExchangeAccountId1",
                principalTable: "ExchangeAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Wallets_WalletId",
                table: "Assets",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Wallets_WalletId1",
                table: "Assets",
                column: "WalletId1",
                principalTable: "Wallets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Assets_FeeAssetId",
                table: "Transactions",
                column: "FeeAssetId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Assets_FromAssetId",
                table: "Transactions",
                column: "FromAssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Assets_ToAssetId",
                table: "Transactions",
                column: "ToAssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ExchangeAccounts_ExchangeAccountId",
                table: "Transactions",
                column: "ExchangeAccountId",
                principalTable: "ExchangeAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId1",
                table: "Transactions",
                column: "WalletId1",
                principalTable: "Wallets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_AspNetUsers_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ExchangeAccounts_ExchangeAccountId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ExchangeAccounts_ExchangeAccountId1",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Wallets_WalletId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Wallets_WalletId1",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_FeeAssetId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_FromAssetId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_ToAssetId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ExchangeAccounts_ExchangeAccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId1",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_AspNetUsers_UserId",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "ExchangeAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ExchangeAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FeeAssetId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FromAssetId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ToAssetId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ExchangeAccountId1",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "BlockchainTxHash",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ExchangeAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ExchangeOrderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FeeAssetId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAddress",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAmount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAssetId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToAddress",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToAssetId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ExchangeAccountId1",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FreeBalance",
                table: "Assets");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "AccountSource");

            migrationBuilder.RenameColumn(
                name: "ToAmount",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "WalletId1",
                table: "Assets",
                newName: "AccountSourceId");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Assets",
                newName: "RetrievedAt");

            migrationBuilder.RenameColumn(
                name: "LockedBalance",
                table: "Assets",
                newName: "Balance");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_WalletId1",
                table: "Assets",
                newName: "IX_Assets_AccountSourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_UserId",
                table: "AccountSource",
                newName: "IX_AccountSource_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountSourceId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "WalletType",
                table: "AccountSource",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddColumn<string>(
                name: "Blockchain",
                table: "AccountSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AccountSource",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExchangeName",
                table: "AccountSource",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountSource",
                table: "AccountSource",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountSourceId",
                table: "Transactions",
                column: "AccountSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AssetId",
                table: "Transactions",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSource_AspNetUsers_UserId",
                table: "AccountSource",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Transactions_AccountSource_AccountSourceId",
                table: "Transactions",
                column: "AccountSourceId",
                principalTable: "AccountSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Assets_AssetId",
                table: "Transactions",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
