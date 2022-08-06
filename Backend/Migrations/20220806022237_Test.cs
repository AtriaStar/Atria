using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaborator",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    WebserviceEntryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborator", x => x.UserEmail);
                    table.ForeignKey(
                        name: "FK_Collaborator_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collaborator_WebserviceEntries_WebserviceEntryName",
                        column: x => x.WebserviceEntryName,
                        principalTable: "WebserviceEntries",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborator_WebserviceEntryName",
                table: "Collaborator",
                column: "WebserviceEntryName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborator");
        }
    }
}
