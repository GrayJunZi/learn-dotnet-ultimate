using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class GetPersons_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp_GetAllPersons = @"
CREATE PROCEDURE [dbo].[sp_GetAllPersons]
AS BEGIN
    SELECT * FROM [dbo].[Persons]
END
";
            migrationBuilder.Sql(sp_GetAllPersons);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp_GetAllPersons = @"DROP PROCEDURE [dbo].[sp_GetAllPersons]";
            migrationBuilder.Sql(sp_GetAllPersons);
        }
    }
}
