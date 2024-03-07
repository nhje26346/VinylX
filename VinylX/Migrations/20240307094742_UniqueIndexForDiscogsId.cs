using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinylX.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndexForDiscogsId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Release_DiscogReleaseId",
                table: "Release",
                column: "DiscogReleaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecordLabel_DiscogLabelId",
                table: "RecordLabel",
                column: "DiscogLabelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterRelease_DiscogMasterReleaseId",
                table: "MasterRelease",
                column: "DiscogMasterReleaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artist_DiscogArtistId",
                table: "Artist",
                column: "DiscogArtistId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Release_DiscogReleaseId",
                table: "Release");

            migrationBuilder.DropIndex(
                name: "IX_RecordLabel_DiscogLabelId",
                table: "RecordLabel");

            migrationBuilder.DropIndex(
                name: "IX_MasterRelease_DiscogMasterReleaseId",
                table: "MasterRelease");

            migrationBuilder.DropIndex(
                name: "IX_Artist_DiscogArtistId",
                table: "Artist");
        }
    }
}
