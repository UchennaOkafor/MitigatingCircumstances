using Microsoft.EntityFrameworkCore.Migrations;

namespace MitigatingCircumstances.Migrations
{
    public partial class ModelsUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportTickets_AspNetUsers_AssignedToId",
                table: "SupportTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTickets_AspNetUsers_CreatedById",
                table: "SupportTickets");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "SupportTickets",
                newName: "TeacherAssignedToId");

            migrationBuilder.RenameColumn(
                name: "AssignedToId",
                table: "SupportTickets",
                newName: "StudentCreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_SupportTickets_CreatedById",
                table: "SupportTickets",
                newName: "IX_SupportTickets_TeacherAssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_SupportTickets_AssignedToId",
                table: "SupportTickets",
                newName: "IX_SupportTickets_StudentCreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTickets_AspNetUsers_StudentCreatedById",
                table: "SupportTickets",
                column: "StudentCreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTickets_AspNetUsers_TeacherAssignedToId",
                table: "SupportTickets",
                column: "TeacherAssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportTickets_AspNetUsers_StudentCreatedById",
                table: "SupportTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTickets_AspNetUsers_TeacherAssignedToId",
                table: "SupportTickets");

            migrationBuilder.RenameColumn(
                name: "TeacherAssignedToId",
                table: "SupportTickets",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "StudentCreatedById",
                table: "SupportTickets",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_SupportTickets_TeacherAssignedToId",
                table: "SupportTickets",
                newName: "IX_SupportTickets_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_SupportTickets_StudentCreatedById",
                table: "SupportTickets",
                newName: "IX_SupportTickets_AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTickets_AspNetUsers_AssignedToId",
                table: "SupportTickets",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTickets_AspNetUsers_CreatedById",
                table: "SupportTickets",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
