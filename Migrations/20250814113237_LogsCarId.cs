using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarKilometerTrack.Migrations
{
    /// <inheritdoc />
    public partial class LogsCarId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id");
        }
    }
}
