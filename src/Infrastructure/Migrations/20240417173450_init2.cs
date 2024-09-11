using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmailGroupId",
                table: "EmailLists",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "EmailGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailGroups_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailLists_EmailGroupId",
                table: "EmailLists",
                column: "EmailGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailGroups_AppUserId",
                table: "EmailGroups",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailLists_EmailGroups_EmailGroupId",
                table: "EmailLists",
                column: "EmailGroupId",
                principalTable: "EmailGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailLists_EmailGroups_EmailGroupId",
                table: "EmailLists");

            migrationBuilder.DropTable(
                name: "EmailGroups");

            migrationBuilder.DropIndex(
                name: "IX_EmailLists_EmailGroupId",
                table: "EmailLists");

            migrationBuilder.DropColumn(
                name: "EmailGroupId",
                table: "EmailLists");
        }
    }
}
