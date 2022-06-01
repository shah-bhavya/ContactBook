using Microsoft.EntityFrameworkCore.Migrations;

namespace Contact.Infrastructure.Data.Migrations
{
    public partial class spGetContactById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetContactById]
                @ContactId varchar(50)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT 
	                c.ContactId,
	                c.Name,
	                c.Email, 
	                c.PhotoPath, 
	                c.Title,

	                STUFF((SELECT '; ' + CAST(p.PhoneNumberId as varchar(200)) + '//' + CAST(p.ContactType as varchar(20)) + '//' + p.Number
                          FROM PhoneNumbers p
                          WHERE p.ContactId = c.ContactId
                          ORDER BY p.Number
                          FOR XML PATH('')), 1, 1, '') [Number]
                FROM Contact c
                Where c.ContactId = @ContactId
                GROUP BY c.ContactId, c.Name, c.Email, c.PhotoPath, c.Title
                ORDER BY 1
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
