using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookUniverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedidtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_UserId1",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_UserId1",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserBook");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserBook",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_UserId",
                table: "UserBook",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_UserId",
                table: "UserBook",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_UserId",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_UserId",
                table: "UserBook");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserBook",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserBook",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_UserId1",
                table: "UserBook",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_UserId1",
                table: "UserBook",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
