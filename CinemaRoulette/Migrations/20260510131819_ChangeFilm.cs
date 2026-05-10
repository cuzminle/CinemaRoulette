using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaRoulette.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Films");

            migrationBuilder.AddColumn<string>(
                name: "Cinema",
                table: "Films",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cinema",
                table: "Films");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Films",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Films",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
