using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes_API.Migrations
{
    public partial class recipeingredientquantitydouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ingredient_measurement_id_fkey",
                table: "ingredient");

            migrationBuilder.AlterColumn<double>(
                name: "quantity",
                table: "recipe_ingredient",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_measurement_measurement_id",
                table: "ingredient",
                column: "measurement_id",
                principalTable: "measurement",
                principalColumn: "measurement_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_measurement_measurement_id",
                table: "ingredient");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "recipe_ingredient",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddForeignKey(
                name: "ingredient_measurement_id_fkey",
                table: "ingredient",
                column: "measurement_id",
                principalTable: "measurement",
                principalColumn: "measurement_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
