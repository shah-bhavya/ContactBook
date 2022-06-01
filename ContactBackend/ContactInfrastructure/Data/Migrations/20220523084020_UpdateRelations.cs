using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contact.Infrastructure.Data.Migrations
{
    public partial class UpdateRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_PhoneNumbers_PhoneNumberId",
                schema: "dbo",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_PhoneNumberId",
                schema: "dbo",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "PhoneNumberId",
                schema: "dbo",
                table: "Contact");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "PhoneNumbers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_ContactId",
                table: "PhoneNumbers",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contact_ContactId",
                table: "PhoneNumbers",
                column: "ContactId",
                principalSchema: "dbo",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contact_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "PhoneNumbers");

            migrationBuilder.AddColumn<Guid>(
                name: "PhoneNumberId",
                schema: "dbo",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PhoneNumberId",
                schema: "dbo",
                table: "Contact",
                column: "PhoneNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_PhoneNumbers_PhoneNumberId",
                schema: "dbo",
                table: "Contact",
                column: "PhoneNumberId",
                principalTable: "PhoneNumbers",
                principalColumn: "PhoneNumberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
