using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EmailSendingStatuses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailSendingStatuses_UserId",
                table: "EmailSendingStatuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSendingStatuses_AspNetUsers_UserId",
                table: "EmailSendingStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailSendingStatuses_AspNetUsers_UserId",
                table: "EmailSendingStatuses");

            migrationBuilder.DropIndex(
                name: "IX_EmailSendingStatuses_UserId",
                table: "EmailSendingStatuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmailSendingStatuses");
        }
    }
}
