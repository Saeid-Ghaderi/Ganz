using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ganz.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Products",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileExtenstion",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFileName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ThumbnailFileSize",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileExtenstion",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailFileSize",
                table: "Products");
        }
    }
}
