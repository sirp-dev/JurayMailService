using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intro1233 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllGroups",
                table: "GroupSendingProjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "EmailGroupId",
                table: "GroupSendingProjects",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupSendingProjects_EmailGroupId",
                table: "GroupSendingProjects",
                column: "EmailGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSendingProjects_EmailGroups_EmailGroupId",
                table: "GroupSendingProjects",
                column: "EmailGroupId",
                principalTable: "EmailGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSendingProjects_EmailGroups_EmailGroupId",
                table: "GroupSendingProjects");

            migrationBuilder.DropIndex(
                name: "IX_GroupSendingProjects_EmailGroupId",
                table: "GroupSendingProjects");

            migrationBuilder.DropColumn(
                name: "AllGroups",
                table: "GroupSendingProjects");

            migrationBuilder.DropColumn(
                name: "EmailGroupId",
                table: "GroupSendingProjects");
        }
    }
}
