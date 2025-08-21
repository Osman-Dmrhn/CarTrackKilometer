using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarKilometerTrack.Migrations
{
    /// <inheritdoc />
    public partial class deleteAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Logs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_cars_CarId",
                table: "Logs",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
