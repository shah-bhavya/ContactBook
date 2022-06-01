using Microsoft.EntityFrameworkCore.Migrations;

namespace Contact.Infrastructure.Data.Migrations
{
    public partial class AddFavouriteField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                schema: "dbo",
                table: "Contact",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavourite",
                schema: "dbo",
                table: "Contact");
        }
    }
}
