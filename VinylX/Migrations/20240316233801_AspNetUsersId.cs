using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinylX.Migrations
{
    /// <inheritdoc />
    public partial class AspNetUsersId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "AspNetUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AspNetUsersId",
                table: "User",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
