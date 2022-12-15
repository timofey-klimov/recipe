using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Persistance.Migrations
{
    public partial class Outbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfirmationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    AcceptedBy = table.Column<int>(type: "int", nullable: true),
                    RejectedBy = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RejectedReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmationRequests_RecipeCards_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "RecipeCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfirmationRequests_Users_AcceptedBy",
                        column: x => x.AcceptedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfirmationRequests_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfirmationRequests_Users_RejectedBy",
                        column: x => x.RejectedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccuredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProccededOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationRequests_AcceptedBy",
                table: "ConfirmationRequests",
                column: "AcceptedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationRequests_CreatedBy",
                table: "ConfirmationRequests",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationRequests_RecipeId",
                table: "ConfirmationRequests",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationRequests_RejectedBy",
                table: "ConfirmationRequests",
                column: "RejectedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmationRequests");

            migrationBuilder.DropTable(
                name: "OutboxMessages");
        }
    }
}
