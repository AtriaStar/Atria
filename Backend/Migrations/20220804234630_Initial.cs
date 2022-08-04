using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    Snowflake = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
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
                    table.PrimaryKey("PK_Drafts", x => x.Snowflake);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    FirstNames = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    ProfilePicture = table.Column<string>(type: "text", nullable: false),
                    Biography = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
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
                    ContactPersonEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebserviceEntries", x => x.Name);
                    table.ForeignKey(
                        name: "FK_WebserviceEntries_Users_ContactPersonEmail",
                        column: x => x.ContactPersonEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Snowflake = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatorEmail = table.Column<string>(type: "text", nullable: false),
                    WebserviceEntryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Snowflake);
                    table.ForeignKey(
                        name: "FK_Questions_Users_CreatorEmail",
                        column: x => x.CreatorEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
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
                    Snowflake = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StarCount = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatorEmail = table.Column<string>(type: "text", nullable: false),
                    WebserviceEntryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Snowflake);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_CreatorEmail",
                        column: x => x.CreatorEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
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
                    Snowflake = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatorEmail = table.Column<string>(type: "text", nullable: false),
                    QuestionSnowflake = table.Column<decimal>(type: "numeric(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Snowflake);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionSnowflake",
                        column: x => x.QuestionSnowflake,
                        principalTable: "Questions",
                        principalColumn: "Snowflake");
                    table.ForeignKey(
                        name: "FK_Answers_Users_CreatorEmail",
                        column: x => x.CreatorEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CreatorEmail",
                table: "Answers",
                column: "CreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionSnowflake",
                table: "Answers",
                column: "QuestionSnowflake");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorEmail",
                table: "Questions",
                column: "CreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_WebserviceEntryName",
                table: "Questions",
                column: "WebserviceEntryName");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatorEmail",
                table: "Reviews",
                column: "CreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WebserviceEntryName",
                table: "Reviews",
                column: "WebserviceEntryName");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_WebserviceEntryName",
                table: "Tags",
                column: "WebserviceEntryName");

            migrationBuilder.CreateIndex(
                name: "IX_WebserviceEntries_ContactPersonEmail",
                table: "WebserviceEntries",
                column: "ContactPersonEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

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
