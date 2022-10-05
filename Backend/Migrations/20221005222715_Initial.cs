using System;
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
                name: "tags",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    first_names = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    profile_picture = table.Column<string>(type: "text", nullable: true),
                    biography = table.Column<string>(type: "text", nullable: true),
                    sign_up_ip = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    rights = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reset_tokens",
                columns: table => new
                {
                    token = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reset_tokens", x => x.token);
                    table.ForeignKey(
                        name: "fk_reset_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    token = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    user_agent = table.Column<string>(type: "text", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sessions", x => x.token);
                    table.ForeignKey(
                        name: "fk_sessions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "webservice_entries",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    short_description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    full_description = table.Column<string>(type: "text", nullable: true),
                    api_check_url = table.Column<string>(type: "text", nullable: true),
                    documentation_link = table.Column<string>(type: "text", nullable: true),
                    documentation = table.Column<string>(type: "text", nullable: true),
                    change_log = table.Column<string>(type: "text", nullable: true),
                    view_count = table.Column<long>(type: "bigint", nullable: false),
                    contact_person_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_webservice_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_webservice_entries_users_contact_person_id",
                        column: x => x.contact_person_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "api_check",
                columns: table => new
                {
                    checked_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    webservice_entry_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_check", x => x.checked_at);
                    table.ForeignKey(
                        name: "fk_api_check_webservice_entries_webservice_entry_id",
                        column: x => x.webservice_entry_id,
                        principalTable: "webservice_entries",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "bookmarks",
                columns: table => new
                {
                    bookmarkees_id = table.Column<long>(type: "bigint", nullable: false),
                    bookmarks_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookmarks", x => new { x.bookmarkees_id, x.bookmarks_id });
                    table.ForeignKey(
                        name: "fk_bookmarks_users_bookmarkees_id",
                        column: x => x.bookmarkees_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bookmarks_webservice_entries_bookmarks_id",
                        column: x => x.bookmarks_id,
                        principalTable: "webservice_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collaborator",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    wse_id = table.Column<long>(type: "bigint", nullable: false),
                    rights = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collaborator", x => new { x.wse_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_collaborator_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_collaborator_webservice_entries_wse_id",
                        column: x => x.wse_id,
                        principalTable: "webservice_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wse_id = table.Column<long>(type: "bigint", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_questions", x => new { x.wse_id, x.id });
                    table.ForeignKey(
                        name: "fk_questions_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_questions_webservice_entries_wse_id",
                        column: x => x.wse_id,
                        principalTable: "webservice_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wse_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    star_count = table.Column<int>(type: "integer", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => new { x.wse_id, x.id });
                    table.ForeignKey(
                        name: "fk_reviews_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_webservice_entries_wse_id",
                        column: x => x.wse_id,
                        principalTable: "webservice_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag_webservice_entry",
                columns: table => new
                {
                    tags_name = table.Column<string>(type: "text", nullable: false),
                    webservice_entries_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag_webservice_entry", x => new { x.tags_name, x.webservice_entries_id });
                    table.ForeignKey(
                        name: "fk_tag_webservice_entry_tags_tags_name",
                        column: x => x.tags_name,
                        principalTable: "tags",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tag_webservice_entry_webservice_entries_webservice_entries_",
                        column: x => x.webservice_entries_id,
                        principalTable: "webservice_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    question_id = table.Column<long>(type: "bigint", nullable: false),
                    wse_id = table.Column<long>(type: "bigint", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    creation_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_answers", x => new { x.wse_id, x.question_id, x.id });
                    table.ForeignKey(
                        name: "fk_answers_questions_wse_id_question_id",
                        columns: x => new { x.wse_id, x.question_id },
                        principalTable: "questions",
                        principalColumns: new[] { "wse_id", "id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_answers_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_answers_webservice_entries_wse_id",
                        column: x => x.wse_id,
                        principalTable: "webservice_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_answers_creator_id",
                table: "answers",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_check_webservice_entry_id",
                table: "api_check",
                column: "webservice_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookmarks_bookmarks_id",
                table: "bookmarks",
                column: "bookmarks_id");

            migrationBuilder.CreateIndex(
                name: "ix_collaborator_user_id",
                table: "collaborator",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_questions_creator_id",
                table: "questions",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_reset_tokens_user_id",
                table: "reset_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_creator_id",
                table: "reviews",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_sessions_user_id",
                table: "sessions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tag_webservice_entry_webservice_entries_id",
                table: "tag_webservice_entry",
                column: "webservice_entries_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_webservice_entries_contact_person_id",
                table: "webservice_entries",
                column: "contact_person_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "api_check");

            migrationBuilder.DropTable(
                name: "bookmarks");

            migrationBuilder.DropTable(
                name: "collaborator");

            migrationBuilder.DropTable(
                name: "reset_tokens");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "tag_webservice_entry");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "webservice_entries");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
