using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokensTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeAccount_User_UserId",
                table: "ExchangeAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_User_UserId",
                table: "Wallet");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeAccount_UserId",
                table: "ExchangeAccount");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Wallet",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ExchangeAccount",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Assets",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageBuyPrice",
                table: "Assets",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId1",
                table: "Wallet",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeAccount_UserId1",
                table: "ExchangeAccount",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId1",
                table: "RefreshTokens",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeAccount_AspNetUsers_UserId1",
                table: "ExchangeAccount",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId1",
                table: "Wallet",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeAccount_AspNetUsers_UserId1",
                table: "ExchangeAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId1",
                table: "Wallet");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId1",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeAccount_UserId1",
                table: "ExchangeAccount");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ExchangeAccount");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldPrecision: 18,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageBuyPrice",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterKeySalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeAccount_UserId",
                table: "ExchangeAccount",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeAccount_User_UserId",
                table: "ExchangeAccount",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_User_UserId",
                table: "Wallet",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
