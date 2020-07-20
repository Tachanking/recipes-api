using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes_Api.Migrations
{
    public partial class RecipeIngredientOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_optional",
                table: "recipe_ingredient",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_optional",
                table: "recipe_ingredient");
        }
    }
}
