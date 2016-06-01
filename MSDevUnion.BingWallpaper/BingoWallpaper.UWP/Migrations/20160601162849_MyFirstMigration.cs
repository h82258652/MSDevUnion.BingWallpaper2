using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BingoWallpaper.Uwp.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoverStory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Attribute = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Parameter1 = table.Column<string>(nullable: true),
                    Parameter2 = table.Column<string>(nullable: true),
                    PrimaryImageUrl = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverStory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ObjectId = table.Column<string>(nullable: false),
                    Copyright = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ErrorCode = table.Column<int>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true),
                    ExistWUXGA = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UrlBase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ObjectId);
                });

            migrationBuilder.CreateTable(
                name: "Archives",
                columns: table => new
                {
                    ObjectId = table.Column<string>(nullable: false),
                    CoverStoryId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    ErrorCode = table.Column<int>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Market = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archives", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_Archives_CoverStory_CoverStoryId",
                        column: x => x.CoverStoryId,
                        principalTable: "CoverStory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotspot",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArchiveObjectId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    LocationX = table.Column<int>(nullable: false),
                    LocationY = table.Column<int>(nullable: false),
                    Query = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotspot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotspot_Archives_ArchiveObjectId",
                        column: x => x.ArchiveObjectId,
                        principalTable: "Archives",
                        principalColumn: "ObjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeanCloudPointer",
                columns: table => new
                {
                    ObjectId = table.Column<string>(nullable: false),
                    ClassName = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeanCloudPointer", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_LeanCloudPointer_Archives_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Archives",
                        principalColumn: "ObjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArchiveObjectId = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Archives_ArchiveObjectId",
                        column: x => x.ArchiveObjectId,
                        principalTable: "Archives",
                        principalColumn: "ObjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Archives_CoverStoryId",
                table: "Archives",
                column: "CoverStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotspot_ArchiveObjectId",
                table: "Hotspot",
                column: "ArchiveObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LeanCloudPointer_ObjectId",
                table: "LeanCloudPointer",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ArchiveObjectId",
                table: "Message",
                column: "ArchiveObjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotspot");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "LeanCloudPointer");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Archives");

            migrationBuilder.DropTable(
                name: "CoverStory");
        }
    }
}
