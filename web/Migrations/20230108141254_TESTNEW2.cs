using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class TESTNEW2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Has_Events_Profile_ProfileID",
                table: "Profile_Has_Events");

            migrationBuilder.DropIndex(
                name: "IX_Profile_Has_Events_ProfileID",
                table: "Profile_Has_Events");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileID",
                table: "Profile_Has_Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Profile_Has_Events",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Has_Events_ApplicationUserId",
                table: "Profile_Has_Events",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Has_Events_Profile_ApplicationUserId",
                table: "Profile_Has_Events",
                column: "ApplicationUserId",
                principalTable: "Profile",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Has_Events_Profile_ApplicationUserId",
                table: "Profile_Has_Events");

            migrationBuilder.DropIndex(
                name: "IX_Profile_Has_Events_ApplicationUserId",
                table: "Profile_Has_Events");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Profile_Has_Events");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileID",
                table: "Profile_Has_Events",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Has_Events_ProfileID",
                table: "Profile_Has_Events",
                column: "ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Has_Events_Profile_ProfileID",
                table: "Profile_Has_Events",
                column: "ProfileID",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
