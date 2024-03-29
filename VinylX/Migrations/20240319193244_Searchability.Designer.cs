﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VinylX.Data;

#nullable disable

namespace VinylX.Migrations
{
    [DbContext(typeof(VinylXContext))]
    [Migration("20240319193244_Searchability")]
    partial class Searchability
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VinylX.Models.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistId"));

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscogArtistId")
                        .HasColumnType("int");

                    b.HasKey("ArtistId");

                    b.HasIndex("DiscogArtistId")
                        .IsUnique();

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("VinylX.Models.Folder", b =>
                {
                    b.Property<int>("FolderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FolderId"));

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FolderId");

                    b.HasIndex("UserId");

                    b.ToTable("Folder");
                });

            modelBuilder.Entity("VinylX.Models.MasterRelease", b =>
                {
                    b.Property<int>("MasterReleaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MasterReleaseId"));

                    b.Property<string>("AlbumName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("BarcodeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscogMasterReleaseId")
                        .HasColumnType("int");

                    b.HasKey("MasterReleaseId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("DiscogMasterReleaseId")
                        .IsUnique();

                    b.ToTable("MasterRelease");
                });

            modelBuilder.Entity("VinylX.Models.Media", b =>
                {
                    b.Property<int>("MediaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MediaID"));

                    b.Property<string>("MediaName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MediaID");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("VinylX.Models.RecordLabel", b =>
                {
                    b.Property<int>("RecordLabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecordLabelId"));

                    b.Property<int>("DiscogLabelId")
                        .HasColumnType("int");

                    b.Property<string>("LabelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecordLabelId");

                    b.HasIndex("DiscogLabelId")
                        .IsUnique();

                    b.HasIndex("LabelName");

                    b.ToTable("RecordLabel");
                });

            modelBuilder.Entity("VinylX.Models.Release", b =>
                {
                    b.Property<int>("ReleaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReleaseId"));

                    b.Property<string>("BarcodeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscogReleaseId")
                        .HasColumnType("int");

                    b.Property<string>("Edition")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MasterReleaseId")
                        .HasColumnType("int");

                    b.Property<int>("MediaID")
                        .HasColumnType("int");

                    b.Property<int?>("RecordLabelId")
                        .HasColumnType("int");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReleaseId");

                    b.HasIndex("DiscogReleaseId")
                        .IsUnique();

                    b.HasIndex("Edition");

                    b.HasIndex("MasterReleaseId");

                    b.HasIndex("MediaID");

                    b.HasIndex("RecordLabelId");

                    b.ToTable("Release");
                });

            modelBuilder.Entity("VinylX.Models.ReleaseInstance", b =>
                {
                    b.Property<int>("ReleaseInstanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReleaseInstanceId"));

                    b.Property<int>("FolderId")
                        .HasColumnType("int");

                    b.Property<string>("Quality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReleaseId")
                        .HasColumnType("int");

                    b.HasKey("ReleaseInstanceId");

                    b.HasIndex("FolderId");

                    b.HasIndex("ReleaseId");

                    b.ToTable("ReleaseInstance");
                });

            modelBuilder.Entity("VinylX.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("AspNetUsersId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("VinylX.Models.Folder", b =>
                {
                    b.HasOne("VinylX.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VinylX.Models.MasterRelease", b =>
                {
                    b.HasOne("VinylX.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("VinylX.Models.Release", b =>
                {
                    b.HasOne("VinylX.Models.MasterRelease", "MasterRelease")
                        .WithMany()
                        .HasForeignKey("MasterReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VinylX.Models.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VinylX.Models.RecordLabel", "RecordLabel")
                        .WithMany()
                        .HasForeignKey("RecordLabelId");

                    b.Navigation("MasterRelease");

                    b.Navigation("Media");

                    b.Navigation("RecordLabel");
                });

            modelBuilder.Entity("VinylX.Models.ReleaseInstance", b =>
                {
                    b.HasOne("VinylX.Models.Folder", "Folder")
                        .WithMany()
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VinylX.Models.Release", "Release")
                        .WithMany()
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("Release");
                });
#pragma warning restore 612, 618
        }
    }
}
