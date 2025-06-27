using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class InsertPerson_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp_InsertPerson = @"
CREATE PROCEDURE [dbo].[sp_InsertPerson]
(
    @PersonId uniqueidentifier,
    @PersonName nvarchar(40),
    @Email nvarchar(50),
    @DateOfBirth datetime2(7),
    @Gender nvarchar(10),
    @CountryId uniqueidentifier,
    @Address nvarchar(1000),
    @ReceiveNewsLetter bit
)
AS BEGIN
    INSERT INTO [dbo].[Person](PersonId,PersonName,Email,DateOfBirth,Gender,CountryId,Address,ReceiveNewsLetter)
    VALUES (@PersonId,@PersonName,@Email,@DateOfBirth,@Gender,@CountryId,@Address,@ReceiveNewsLetter)
END
";
            migrationBuilder.Sql(sp_InsertPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp_InsertPerson = @"DROP PROCEDURE [dbo].[sp_InsertPerson]";
            migrationBuilder.Sql(sp_InsertPerson);
        }
    }
}
