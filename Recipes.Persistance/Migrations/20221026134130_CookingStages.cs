using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Persistance.Migrations
{
    public partial class CookingStages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CookingProcess",
                table: "RecipeCardDetails",
                newName: "Remark");

            migrationBuilder.AddColumn<string>(
                name: "MealType",
                table: "RecipeCardDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CookingStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookingTime = table.Column<double>(type: "float", nullable: false),
                    RecipeDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingStages_RecipeCardDetails_RecipeDetailsId",
                        column: x => x.RecipeDetailsId,
                        principalTable: "RecipeCardDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CookingStages_RecipeDetailsId",
                table: "CookingStages",
                column: "RecipeDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookingStageImages");

            migrationBuilder.DropTable(
                name: "CookingStages");

            migrationBuilder.DropColumn(
                name: "MealType",
                table: "RecipeCardDetails");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "RecipeCardDetails",
                newName: "CookingProcess");
        }
    }
}
