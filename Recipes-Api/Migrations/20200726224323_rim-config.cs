using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Recipes_Api.Migrations
{
    public partial class rimconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "measurement_id",
                table: "recipe_ingredient_measurement",
                newName: "MeasurementId");

            migrationBuilder.RenameColumn(
                name: "ingredient_id",
                table: "recipe_ingredient_measurement",
                newName: "IngredientId");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                table: "recipe_ingredient_measurement",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_ingredient_measurement_measurement_id",
                table: "recipe_ingredient_measurement",
                newName: "IX_recipe_ingredient_measurement_MeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_ingredient_measurement_ingredient_id",
                table: "recipe_ingredient_measurement",
                newName: "IX_recipe_ingredient_measurement_IngredientId");

            migrationBuilder.AlterColumn<long>(
                name: "MeasurementId",
                table: "recipe_ingredient_measurement",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "IngredientId",
                table: "recipe_ingredient_measurement",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "RecipeId",
                table: "recipe_ingredient_measurement",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeasurementId",
                table: "recipe_ingredient_measurement",
                newName: "measurement_id");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "recipe_ingredient_measurement",
                newName: "ingredient_id");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "recipe_ingredient_measurement",
                newName: "recipe_id");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_ingredient_measurement_MeasurementId",
                table: "recipe_ingredient_measurement",
                newName: "IX_recipe_ingredient_measurement_measurement_id");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_ingredient_measurement_IngredientId",
                table: "recipe_ingredient_measurement",
                newName: "IX_recipe_ingredient_measurement_ingredient_id");

            migrationBuilder.AlterColumn<long>(
                name: "measurement_id",
                table: "recipe_ingredient_measurement",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "ingredient_id",
                table: "recipe_ingredient_measurement",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "recipe_id",
                table: "recipe_ingredient_measurement",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
