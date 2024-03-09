using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinylX.Migrations
{
    /// <inheritdoc />
    public partial class MoveFieldsfromMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryNumber",
                table: "MasterRelease");

            migrationBuilder.AddColumn<string>(
                name: "BarcodeNumber",
                table: "Release",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcodeNumber",
                table: "Release");

            migrationBuilder.AddColumn<string>(
                name: "CategoryNumber",
                table: "MasterRelease",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
