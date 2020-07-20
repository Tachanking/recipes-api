using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Recipes_Api.Migrations
{
    public partial class recipeingredientmeasurement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_measurement_measurement_id",
                table: "ingredient");

            migrationBuilder.DropTable(
                name: "recipe_ingredient");

            migrationBuilder.DropIndex(
                name: "IX_ingredient_measurement_id",
                table: "ingredient");

            migrationBuilder.DropColumn(
                name: "measurement_id",
                table: "ingredient");

            migrationBuilder.CreateTable(
                name: "recipe_ingredient_measurement",
                columns: table => new
                {
                    recipe_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredient_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    measurement_id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recipe_ingredient_measurement_pkey", x => new { x.recipe_id, x.ingredient_id, x.measurement_id });
                    table.ForeignKey(
                        name: "recipe_ingredient_measurement_ingredient_id_fkey",
                        column: x => x.ingredient_id,
                        principalTable: "ingredient",
                        principalColumn: "ingredient_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "recipe_ingredient_measurement_measurement_id_fkey",
                        column: x => x.measurement_id,
                        principalTable: "measurement",
                        principalColumn: "measurement_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "recipe_ingredient_measurement_recipe_id_fkey",
                        column: x => x.recipe_id,
                        principalTable: "recipe",
                        principalColumn: "recipe_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredient_measurement_ingredient_id",
                table: "recipe_ingredient_measurement",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredient_measurement_measurement_id",
                table: "recipe_ingredient_measurement",
                column: "measurement_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipe_ingredient_measurement");

            migrationBuilder.AddColumn<long>(
                name: "measurement_id",
                table: "ingredient",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "recipe_ingredient",
                columns: table => new
                {
                    recipe_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredient_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recipe_ingredient_pkey", x => new { x.recipe_id, x.ingredient_id });
                    table.ForeignKey(
                        name: "recipe_ingredient_ingredient_id_fkey",
                        column: x => x.ingredient_id,
                        principalTable: "ingredient",
                        principalColumn: "ingredient_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "recipe_ingredient_recipe_id_fkey",
                        column: x => x.recipe_id,
                        principalTable: "recipe",
                        principalColumn: "recipe_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_measurement_id",
                table: "ingredient",
                column: "measurement_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredient_ingredient_id",
                table: "recipe_ingredient",
                column: "ingredient_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_measurement_measurement_id",
                table: "ingredient",
                column: "measurement_id",
                principalTable: "measurement",
                principalColumn: "measurement_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
