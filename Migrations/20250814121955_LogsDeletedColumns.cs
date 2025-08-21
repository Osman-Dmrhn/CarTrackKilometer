using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarKilometerTrack.Migrations
{
    /// <inheritdoc />
    public partial class LogsDeletedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_users_UserId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_CarId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_UserId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_CarId",
                table: "Logs",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_users_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
