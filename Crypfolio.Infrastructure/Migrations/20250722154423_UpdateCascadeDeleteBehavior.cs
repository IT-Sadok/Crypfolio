using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSource_AspNetUsers_UserId",
                table: "AccountSource");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AccountSource",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSource_AspNetUsers_UserId",
                table: "AccountSource",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSource_AspNetUsers_UserId",
                table: "AccountSource");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AccountSource");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSource_AspNetUsers_UserId",
                table: "AccountSource",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
