using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAssetFKFromTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_FeeAssetId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_FromAssetId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_ToAssetId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FeeAssetId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FeeAssetId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "ToAssetId",
                table: "Transactions",
                newName: "AssetId1");

            migrationBuilder.RenameColumn(
                name: "FromAssetId",
                table: "Transactions",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ToAssetId",
                table: "Transactions",
                newName: "IX_Transactions_AssetId1");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_FromAssetId",
                table: "Transactions",
                newName: "IX_Transactions_AssetId");

            migrationBuilder.AddColumn<string>(
                name: "FeeAsset",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromAsset",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToAsset",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Assets_AssetId",
                table: "Transactions",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Assets_AssetId1",
                table: "Transactions",
                column: "AssetId1",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_AssetId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_AssetId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FeeAsset",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAsset",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToAsset",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "AssetId1",
                table: "Transactions",
                newName: "ToAssetId");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "Transactions",
                newName: "FromAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AssetId1",
                table: "Transactions",
                newName: "IX_Transactions_ToAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AssetId",
                table: "Transactions",
                newName: "IX_Transactions_FromAssetId");

            migrationBuilder.AddColumn<Guid>(
                name: "FeeAssetId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FeeAssetId",
                table: "Transactions",
                column: "FeeAssetId");

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
        }
    }
}
