using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace initproject.Migrations
{
    /// <inheritdoc />
    public partial class change_in_model_film : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                table: "Films",
                newName: "Overview");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Films",
                newName: "OriginalLanguage");

            migrationBuilder.AlterColumn<string>(
                name: "ReleaseDate",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Overview",
                table: "Films",
                newName: "Language");

            migrationBuilder.RenameColumn(
                name: "OriginalLanguage",
                table: "Films",
                newName: "Description");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Films",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
