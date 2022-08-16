﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    FullDescription = table.Column<string>(type: "text", nullable: false),
                    DocumentationLink = table.Column<string>(type: "text", nullable: false),
                    Changelog = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    FirstNames = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ProfilePicture = table.Column<string>(type: "text", nullable: false),
                    Biography = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebserviceEntries",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    FullDescription = table.Column<string>(type: "text", nullable: false),
                    DocumentationLink = table.Column<string>(type: "text", nullable: false),
                    Changelog = table.Column<string>(type: "text", nullable: false),
                    ViewCount = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ContactPersonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebserviceEntries", x => x.Name);
                    table.ForeignKey(
                        name: "FK_WebserviceEntries_Users_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collaborator",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    WebserviceEntryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborator", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Collaborator_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collaborator_WebserviceEntries_WebserviceEntryName",
                        column: x => x.WebserviceEntryName,
                        principalTable: "WebserviceEntries",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    WebserviceEntryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_WebserviceEntries_WebserviceEntryName",
                        column: x => x.WebserviceEntryName,
                        principalTable: "WebserviceEntries",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StarCount = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    WebserviceEntryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_WebserviceEntries_WebserviceEntryName",
                        column: x => x.WebserviceEntryName,
                        principalTable: "WebserviceEntries",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UseCount = table.Column<long>(type: "bigint", nullable: false),
                    WebserviceEntryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Tags_WebserviceEntries_WebserviceEntryName",
                        column: x => x.WebserviceEntryName,
                        principalTable: "WebserviceEntries",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answers_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CreatorId",
                table: "Answers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborator_WebserviceEntryName",
                table: "Collaborator",
                column: "WebserviceEntryName");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorId",
                table: "Questions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_WebserviceEntryName",
                table: "Questions",
                column: "WebserviceEntryName");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatorId",
                table: "Reviews",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WebserviceEntryName",
                table: "Reviews",
                column: "WebserviceEntryName");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_WebserviceEntryName",
                table: "Tags",
                column: "WebserviceEntryName");

            migrationBuilder.CreateIndex(
                name: "IX_WebserviceEntries_ContactPersonId",
                table: "WebserviceEntries",
                column: "ContactPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Collaborator");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "WebserviceEntries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}