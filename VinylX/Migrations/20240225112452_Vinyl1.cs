using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinylX.Migrations
{
    /// <inheritdoc />
    public partial class Vinyl1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseID",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ReleaseInstance",
                newName: "ReleaseId");

            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "ReleaseInstance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MasterReleaseId",
                table: "Release",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MediaID",
                table: "Release",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecordLabelId",
                table: "Release",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BarcodeNumber",
                table: "MasterRelease",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CategoryNumber",
                table: "MasterRelease",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscogMasterReleaseId",
                table: "MasterRelease",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Folder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseInstance_FolderId",
                table: "ReleaseInstance",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseInstance_ReleaseId",
                table: "ReleaseInstance",
                column: "ReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Release_MasterReleaseId",
                table: "Release",
                column: "MasterReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Release_MediaID",
                table: "Release",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_Release_RecordLabelId",
                table: "Release",
                column: "RecordLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterRelease_ArtistId",
                table: "MasterRelease",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Folder_UserId",
                table: "Folder",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folder_User_UserId",
                table: "Folder",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterRelease_Artist_ArtistId",
                table: "MasterRelease",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Release_MasterRelease_MasterReleaseId",
                table: "Release",
                column: "MasterReleaseId",
                principalTable: "MasterRelease",
                principalColumn: "MasterReleaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Release_Media_MediaID",
                table: "Release",
                column: "MediaID",
                principalTable: "Media",
                principalColumn: "MediaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Release_RecordLabel_RecordLabelId",
                table: "Release",
                column: "RecordLabelId",
                principalTable: "RecordLabel",
                principalColumn: "RecordLabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseInstance_Folder_FolderId",
                table: "ReleaseInstance",
                column: "FolderId",
                principalTable: "Folder",
                principalColumn: "FolderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseInstance_Release_ReleaseId",
                table: "ReleaseInstance",
                column: "ReleaseId",
                principalTable: "Release",
                principalColumn: "ReleaseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folder_User_UserId",
                table: "Folder");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterRelease_Artist_ArtistId",
                table: "MasterRelease");

            migrationBuilder.DropForeignKey(
                name: "FK_Release_MasterRelease_MasterReleaseId",
                table: "Release");

            migrationBuilder.DropForeignKey(
                name: "FK_Release_Media_MediaID",
                table: "Release");

            migrationBuilder.DropForeignKey(
                name: "FK_Release_RecordLabel_RecordLabelId",
                table: "Release");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseInstance_Folder_FolderId",
                table: "ReleaseInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseInstance_Release_ReleaseId",
                table: "ReleaseInstance");

            migrationBuilder.DropIndex(
                name: "IX_ReleaseInstance_FolderId",
                table: "ReleaseInstance");

            migrationBuilder.DropIndex(
                name: "IX_ReleaseInstance_ReleaseId",
                table: "ReleaseInstance");

            migrationBuilder.DropIndex(
                name: "IX_Release_MasterReleaseId",
                table: "Release");

            migrationBuilder.DropIndex(
                name: "IX_Release_MediaID",
                table: "Release");

            migrationBuilder.DropIndex(
                name: "IX_Release_RecordLabelId",
                table: "Release");

            migrationBuilder.DropIndex(
                name: "IX_MasterRelease_ArtistId",
                table: "MasterRelease");

            migrationBuilder.DropIndex(
                name: "IX_Folder_UserId",
                table: "Folder");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "ReleaseInstance");

            migrationBuilder.DropColumn(
                name: "MasterReleaseId",
                table: "Release");

            migrationBuilder.DropColumn(
                name: "MediaID",
                table: "Release");

            migrationBuilder.DropColumn(
                name: "RecordLabelId",
                table: "Release");

            migrationBuilder.DropColumn(
                name: "CategoryNumber",
                table: "MasterRelease");

            migrationBuilder.DropColumn(
                name: "DiscogMasterReleaseId",
                table: "MasterRelease");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Folder");

            migrationBuilder.RenameColumn(
                name: "ReleaseId",
                table: "ReleaseInstance",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseID",
                table: "Media",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BarcodeNumber",
                table: "MasterRelease",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
