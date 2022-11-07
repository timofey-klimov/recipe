using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Persistance.Migrations
{
    public partial class DropRecipeCardImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeMainImages");

            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "RecipeCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "RecipeCards");

            migrationBuilder.CreateTable(
                name: "RecipeMainImages",
                columns: table => new
                {
                    RecipeCardId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMainImages", x => x.RecipeCardId);
                    table.ForeignKey(
                        name: "FK_RecipeMainImages_RecipeCards_RecipeCardId",
                        column: x => x.RecipeCardId,
                        principalTable: "RecipeCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
