using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class evhasactiv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Has_activity_Event_EventID",
                table: "Event_Has_activity");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "Event_Has_activity",
                newName: "eventID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Has_activity_EventID",
                table: "Event_Has_activity",
                newName: "IX_Event_Has_activity_eventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Has_activity_Event_eventID",
                table: "Event_Has_activity",
                column: "eventID",
                principalTable: "Event",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Has_activity_Event_eventID",
                table: "Event_Has_activity");

            migrationBuilder.RenameColumn(
                name: "eventID",
                table: "Event_Has_activity",
                newName: "EventID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Has_activity_eventID",
                table: "Event_Has_activity",
                newName: "IX_Event_Has_activity_EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Has_activity_Event_EventID",
                table: "Event_Has_activity",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
