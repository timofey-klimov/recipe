using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Persistance.Migrations
{
    public partial class DropCookingImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookingStageImages");

            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "CookingStages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "CookingStages");

            migrationBuilder.CreateTable(
                name: "CookingStageImages",
                columns: table => new
                {
                    CookingStageId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingStageImages", x => x.CookingStageId);
                    table.ForeignKey(
                        name: "FK_CookingStageImages_CookingStages_CookingStageId",
                        column: x => x.CookingStageId,
                        principalTable: "CookingStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
