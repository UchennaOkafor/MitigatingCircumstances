using Microsoft.EntityFrameworkCore.Migrations;

namespace MitigatingCircumstances.Migrations
{
    public partial class UploadedDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadedDocument_SupportTickets_SupportTicketId",
                table: "UploadedDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadedDocument",
                table: "UploadedDocument");

            migrationBuilder.RenameTable(
                name: "UploadedDocument",
                newName: "UploadedDocuments");

            migrationBuilder.RenameColumn(
                name: "UploadedBy",
                table: "UploadedDocuments",
                newName: "UploadedById");

            migrationBuilder.RenameIndex(
                name: "IX_UploadedDocument_SupportTicketId",
                table: "UploadedDocuments",
                newName: "IX_UploadedDocuments_SupportTicketId");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedById",
                table: "UploadedDocuments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadedDocuments",
                table: "UploadedDocuments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedDocuments_UploadedById",
                table: "UploadedDocuments",
                column: "UploadedById");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedDocuments_SupportTickets_SupportTicketId",
                table: "UploadedDocuments",
                column: "SupportTicketId",
                principalTable: "SupportTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedDocuments_AspNetUsers_UploadedById",
                table: "UploadedDocuments",
                column: "UploadedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadedDocuments_SupportTickets_SupportTicketId",
                table: "UploadedDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadedDocuments_AspNetUsers_UploadedById",
                table: "UploadedDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadedDocuments",
                table: "UploadedDocuments");

            migrationBuilder.DropIndex(
                name: "IX_UploadedDocuments_UploadedById",
                table: "UploadedDocuments");

            migrationBuilder.RenameTable(
                name: "UploadedDocuments",
                newName: "UploadedDocument");

            migrationBuilder.RenameColumn(
                name: "UploadedById",
                table: "UploadedDocument",
                newName: "UploadedBy");

            migrationBuilder.RenameIndex(
                name: "IX_UploadedDocuments_SupportTicketId",
                table: "UploadedDocument",
                newName: "IX_UploadedDocument_SupportTicketId");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "UploadedDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadedDocument",
                table: "UploadedDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedDocument_SupportTickets_SupportTicketId",
                table: "UploadedDocument",
                column: "SupportTicketId",
                principalTable: "SupportTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
