using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarKilometerTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddInspectionInsurance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Inspection",
                table: "cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Insurance",
                table: "cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inspection",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "cars");
        }
    }
}
