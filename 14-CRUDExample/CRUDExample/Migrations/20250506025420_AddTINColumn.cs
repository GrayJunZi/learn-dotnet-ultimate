using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDExample.Migrations
{
    /// <inheritdoc />
    public partial class AddTINColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TIN",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("32859a69-4bdd-489d-9912-f2dab6b64270"),
                columns: new[] { "DateOfBirth", "TIN" },
                values: new object[] { new DateTime(2005, 5, 6, 10, 54, 19, 522, DateTimeKind.Local).AddTicks(5242), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TIN",
                table: "Persons");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("32859a69-4bdd-489d-9912-f2dab6b64270"),
                column: "DateOfBirth",
                value: new DateTime(2005, 5, 6, 9, 5, 22, 564, DateTimeKind.Local).AddTicks(2768));
        }
    }
}
