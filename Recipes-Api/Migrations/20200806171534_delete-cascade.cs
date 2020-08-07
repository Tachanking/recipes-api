using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes_Api.Migrations
{
    public partial class deletecascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "recipe_ingredient_measurement_ingredient_id_fkey",
                table: "recipe_ingredient_measurement");

            migrationBuilder.DropForeignKey(
                name: "recipe_ingredient_measurement_measurement_id_fkey",
                table: "recipe_ingredient_measurement");

            migrationBuilder.DropForeignKey(
                name: "recipe_ingredient_measurement_recipe_id_fkey",
                table: "recipe_ingredient_measurement");

            migrationBuilder.AddForeignKey(
                name: "recipe_ingredient_measurement_ingredient_id_fkey",
                table: "recipe_ingredient_measurement",
                column: "IngredientId",
                principalTable: "ingredient",
                principalColumn: "ingredient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "recipe_ingredient_measurement_measurement_id_fkey",
                table: "recipe_ingredient_measurement",
                column: "MeasurementId",
                principalTable: "measurement",
                principalColumn: "measurement_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "recipe_ingredient_measurement_recipe_id_fkey",
                table: "recipe_ingredient_measurement",
                column: "RecipeId",
                principalTable: "recipe",
                principalColumn: "recipe_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "recipe_ingredient_measurement_ingredient_id_fkey",
                table: "recipe_ingredient_measurement");

            migrationBuilder.DropForeignKey(
                name: "recipe_ingredient_measurement_measurement_id_fkey",
                table: "recipe_ingredient_measurement");

            migrationBuilder.DropForeignKey(
                name: "recipe_ingredient_measurement_recipe_id_fkey",
                table: "recipe_ingredient_measurement");

            migrationBuilder.AddForeignKey(
                name: "recipe_ingredient_measurement_ingredient_id_fkey",
                table: "recipe_ingredient_measurement",
                column: "IngredientId",
                principalTable: "ingredient",
                principalColumn: "ingredient_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "recipe_ingredient_measurement_measurement_id_fkey",
                table: "recipe_ingredient_measurement",
                column: "MeasurementId",
                principalTable: "measurement",
                principalColumn: "measurement_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "recipe_ingredient_measurement_recipe_id_fkey",
                table: "recipe_ingredient_measurement",
                column: "RecipeId",
                principalTable: "recipe",
                principalColumn: "recipe_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
