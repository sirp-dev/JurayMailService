using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intro12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disable",
                table: "Servers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Servers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmailProjectId",
                table: "EmailSendingStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Servers_UserId",
                table: "Servers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSendingStatuses_EmailProjectId",
                table: "EmailSendingStatuses",
                column: "EmailProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSendingStatuses_EmailProjects_EmailProjectId",
                table: "EmailSendingStatuses",
                column: "EmailProjectId",
                principalTable: "EmailProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_AspNetUsers_UserId",
                table: "Servers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailSendingStatuses_EmailProjects_EmailProjectId",
                table: "EmailSendingStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Servers_AspNetUsers_UserId",
                table: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_Servers_UserId",
                table: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_EmailSendingStatuses_EmailProjectId",
                table: "EmailSendingStatuses");

            migrationBuilder.DropColumn(
                name: "Disable",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "EmailProjectId",
                table: "EmailSendingStatuses");
        }
    }
}
