using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contact.Infrastructure.Data.Migrations
{
    public partial class UpdateContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contact_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "dbo",
                table: "Contact");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "PhoneNumbers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contact_ContactId",
                table: "PhoneNumbers",
                column: "ContactId",
                principalSchema: "dbo",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contact_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "PhoneNumbers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "dbo",
                table: "Contact",
                type: "varchar(15)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contact_ContactId",
                table: "PhoneNumbers",
                column: "ContactId",
                principalSchema: "dbo",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
