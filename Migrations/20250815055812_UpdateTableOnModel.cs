using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarKilometerTrack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableOnModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "carId",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "InUse",
                table: "cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InUseId",
                table: "cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InUseUserId",
                table: "cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UseNote",
                table: "cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_cars_carId",
                        column: x => x.carId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_carId",
                table: "Logs",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_userId",
                table: "Logs",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_InUseUserId",
                table: "cars",
                column: "InUseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_carId",
                table: "Notes",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_userId",
                table: "Notes",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_users_InUseUserId",
                table: "cars",
                column: "InUseUserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_cars_carId",
                table: "Logs",
                column: "carId",
                principalTable: "cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_users_userId",
                table: "Logs",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_users_InUseUserId",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_cars_carId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_users_userId",
                table: "Logs");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Logs_carId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_userId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_cars_InUseUserId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "users");

            migrationBuilder.DropColumn(
                name: "carId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "InUse",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "InUseId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "InUseUserId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "UseNote",
                table: "cars");
        }
    }
}
