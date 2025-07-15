using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Images");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Images",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
