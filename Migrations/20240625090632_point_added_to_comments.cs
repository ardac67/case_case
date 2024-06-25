using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace initproject.Migrations
{
    /// <inheritdoc />
    public partial class point_added_to_comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "Comments");
        }
    }
}
