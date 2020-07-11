using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Recipes_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:adminpack", ",,");

            migrationBuilder.CreateTable(
                name: "measurement",
                columns: table => new
                {
                    measurement_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    measurement_name = table.Column<string>(maxLength: 64, nullable: false),
                    measurement_symbol = table.Column<string>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measurement", x => x.measurement_id);
                });

            migrationBuilder.CreateTable(
                name: "recipe",
                columns: table => new
                {
                    recipe_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipe_name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe", x => x.recipe_id);
                });

            migrationBuilder.CreateTable(
                name: "tool",
                columns: table => new
                {
                    tool_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tool_name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tool", x => x.tool_id);
                });

            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    ingredient_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    measurement_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredient_name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.ingredient_id);
                    table.ForeignKey(
                        name: "ingredient_measurement_id_fkey",
                        column: x => x.measurement_id,
                        principalTable: "measurement",
                        principalColumn: "measurement_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recipe_tool",
                columns: table => new
                {
                    recipe_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tool_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recipe_tool_pkey", x => new { x.recipe_id, x.tool_id });
                    table.ForeignKey(
                        name: "recipe_tool_recipe_id_fkey",
                        column: x => x.recipe_id,
                        principalTable: "recipe",
                        principalColumn: "recipe_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "recipe_tool_tool_id_fkey",
                        column: x => x.tool_id,
                        principalTable: "tool",
                        principalColumn: "tool_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recipe_ingredient",
                columns: table => new
                {
                    recipe_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredient_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_recipe_tool_tool_id",
                table: "recipe_tool",
                column: "tool_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipe_ingredient");

            migrationBuilder.DropTable(
                name: "recipe_tool");

            migrationBuilder.DropTable(
                name: "ingredient");

            migrationBuilder.DropTable(
                name: "recipe");

            migrationBuilder.DropTable(
                name: "tool");

            migrationBuilder.DropTable(
                name: "measurement");
        }
    }
}
