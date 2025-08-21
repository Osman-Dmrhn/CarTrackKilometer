using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarKilometerTrack.Migrations
{
    /// <inheritdoc />
    public partial class carUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InUseId",
                table: "cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InUseId",
                table: "cars",
                type: "int",
                nullable: true);
        }
    }
}
