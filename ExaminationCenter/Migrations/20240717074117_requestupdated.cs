using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationCenter.Migrations
{
    /// <inheritdoc />
    public partial class requestupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_requests_users_UserId",
                table: "requests");

            migrationBuilder.DropIndex(
                name: "IX_requests_UserId",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "requests");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "requests",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "requests");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_requests_UserId",
                table: "requests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_requests_users_UserId",
                table: "requests",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
