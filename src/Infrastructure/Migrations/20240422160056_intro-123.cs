using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intro123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "EmailSendingStatuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Retries",
                table: "EmailSendingStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupSendingProjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailProjectId = table.Column<long>(type: "bigint", nullable: false),
                    Submitted = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSendingProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupSendingProjects_EmailProjects_EmailProjectId",
                        column: x => x.EmailProjectId,
                        principalTable: "EmailProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupSendingProjects_EmailProjectId",
                table: "GroupSendingProjects",
                column: "EmailProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSendingProjects");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "EmailSendingStatuses");

            migrationBuilder.DropColumn(
                name: "Retries",
                table: "EmailSendingStatuses");
        }
    }
}
