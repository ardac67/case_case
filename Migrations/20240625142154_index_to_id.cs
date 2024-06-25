using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace initproject.Migrations
{
    /// <inheritdoc />
    public partial class index_to_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "test_db",
                table: "Films",
                newName: "IX_Film_ReleaseDate");

            migrationBuilder.CreateIndex(
                name: "IX_Film_Id",
                table: "Films",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Film_Id",
                table: "Films");

            migrationBuilder.RenameIndex(
                name: "IX_Film_ReleaseDate",
                table: "Films",
                newName: "test_db");
        }
    }
}
