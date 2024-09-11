using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class uodatehook1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageId",
                table: "EmailResponseStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EmailResponseStatuses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailResponseStatuses_UserId",
                table: "EmailResponseStatuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailResponseStatuses_AspNetUsers_UserId",
                table: "EmailResponseStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailResponseStatuses_AspNetUsers_UserId",
                table: "EmailResponseStatuses");

            migrationBuilder.DropIndex(
                name: "IX_EmailResponseStatuses_UserId",
                table: "EmailResponseStatuses");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "EmailResponseStatuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmailResponseStatuses");
        }
    }
}
