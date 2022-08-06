using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class Snowflake_to_Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionSnowflake",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_CreatorEmail",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborator_Users_UserEmail",
                table: "Collaborator");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatorEmail",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CreatorEmail",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_WebserviceEntries_Users_ContactPersonEmail",
                table: "WebserviceEntries");

            migrationBuilder.DropIndex(
                name: "IX_WebserviceEntries_ContactPersonEmail",
                table: "WebserviceEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CreatorEmail",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorEmail",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaborator",
                table: "Collaborator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CreatorEmail",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ContactPersonEmail",
                table: "WebserviceEntries");

            migrationBuilder.DropColumn(
                name: "CreatorEmail",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatorEmail",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Collaborator");

            migrationBuilder.DropColumn(
                name: "CreatorEmail",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "Snowflake",
                table: "Reviews",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "Snowflake",
                table: "Questions",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "Snowflake",
                table: "Drafts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QuestionSnowflake",
                table: "Answers",
                newName: "QuestionId");

            migrationBuilder.RenameColumn(
                name: "Snowflake",
                table: "Answers",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionSnowflake",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.AddColumn<decimal>(
                name: "ContactPersonId",
                table: "WebserviceEntries",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "Users",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "Reviews",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "Questions",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UserId",
                table: "Collaborator",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "Answers",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaborator",
                table: "Collaborator",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WebserviceEntries_ContactPersonId",
                table: "WebserviceEntries",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatorId",
                table: "Reviews",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorId",
                table: "Questions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CreatorId",
                table: "Answers",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_CreatorId",
                table: "Answers",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborator_Users_UserId",
                table: "Collaborator",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatorId",
                table: "Questions",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CreatorId",
                table: "Reviews",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebserviceEntries_Users_ContactPersonId",
                table: "WebserviceEntries",
                column: "ContactPersonId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_CreatorId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborator_Users_UserId",
                table: "Collaborator");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatorId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CreatorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_WebserviceEntries_Users_ContactPersonId",
                table: "WebserviceEntries");

            migrationBuilder.DropIndex(
                name: "IX_WebserviceEntries_ContactPersonId",
                table: "WebserviceEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CreatorId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaborator",
                table: "Collaborator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CreatorId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ContactPersonId",
                table: "WebserviceEntries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Collaborator");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Reviews",
                newName: "Snowflake");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Questions",
                newName: "Snowflake");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Drafts",
                newName: "Snowflake");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Answers",
                newName: "QuestionSnowflake");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Answers",
                newName: "Snowflake");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionSnowflake");

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonEmail",
                table: "WebserviceEntries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmail",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmail",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Collaborator",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmail",
                table: "Answers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Snowflake");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Snowflake");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaborator",
                table: "Collaborator",
                column: "UserEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Snowflake");

            migrationBuilder.CreateIndex(
                name: "IX_WebserviceEntries_ContactPersonEmail",
                table: "WebserviceEntries",
                column: "ContactPersonEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatorEmail",
                table: "Reviews",
                column: "CreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorEmail",
                table: "Questions",
                column: "CreatorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CreatorEmail",
                table: "Answers",
                column: "CreatorEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionSnowflake",
                table: "Answers",
                column: "QuestionSnowflake",
                principalTable: "Questions",
                principalColumn: "Snowflake");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_CreatorEmail",
                table: "Answers",
                column: "CreatorEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborator_Users_UserEmail",
                table: "Collaborator",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatorEmail",
                table: "Questions",
                column: "CreatorEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CreatorEmail",
                table: "Reviews",
                column: "CreatorEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebserviceEntries_Users_ContactPersonEmail",
                table: "WebserviceEntries",
                column: "ContactPersonEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }
    }
}
