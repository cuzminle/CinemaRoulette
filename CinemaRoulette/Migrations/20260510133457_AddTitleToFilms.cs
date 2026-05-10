using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaRoulette.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Films",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Films");
        }
    }
}
