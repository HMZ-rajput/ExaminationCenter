using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationCenter.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyaddedinreport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReqId",
                table: "examinations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_examinations_ReqId",
                table: "examinations",
                column: "ReqId");

            migrationBuilder.AddForeignKey(
                name: "FK_examinations_requests_ReqId",
                table: "examinations",
                column: "ReqId",
                principalTable: "requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_examinations_requests_ReqId",
                table: "examinations");

            migrationBuilder.DropIndex(
                name: "IX_examinations_ReqId",
                table: "examinations");

            migrationBuilder.DropColumn(
                name: "ReqId",
                table: "examinations");
        }
    }
}
