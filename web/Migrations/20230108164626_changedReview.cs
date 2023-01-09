using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class changedReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Profile_ProfileId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ProfileId",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Review",
                newName: "ProfileID");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileID",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Review",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_ApplicationUserId",
                table: "Review",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Profile_ApplicationUserId",
                table: "Review",
                column: "ApplicationUserId",
                principalTable: "Profile",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Profile_ApplicationUserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ApplicationUserId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "Review",
                newName: "ProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "Review",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProfileId",
                table: "Review",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Profile_ProfileId",
                table: "Review",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
