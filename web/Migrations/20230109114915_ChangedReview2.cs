using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class ChangedReview2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Profile_ApplicationUserId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ProfileID",
                table: "Review");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Review",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Profile_ApplicationUserId",
                table: "Review",
                column: "ApplicationUserId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Profile_ApplicationUserId",
                table: "Review");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Review",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProfileID",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Profile_ApplicationUserId",
                table: "Review",
                column: "ApplicationUserId",
                principalTable: "Profile",
                principalColumn: "Id");
        }
    }
}
