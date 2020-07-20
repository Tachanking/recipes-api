using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Recipes_Api.Migrations
{
    public partial class OwnedIngredientContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedIngredient",
                table: "OwnedIngredient");

            migrationBuilder.RenameTable(
                name: "OwnedIngredient",
                newName: "owned_ingredient");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "owned_ingredient",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "owned_ingredient",
                newName: "ingredient_id");

            migrationBuilder.RenameColumn(
                name: "OwnedIngredientId",
                table: "owned_ingredient",
                newName: "owned_ingredient_id");

            migrationBuilder.AlterColumn<int>(
                name: "ingredient_id",
                table: "owned_ingredient",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "owned_ingredient_pkey",
                table: "owned_ingredient",
                column: "owned_ingredient_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "owned_ingredient_pkey",
                table: "owned_ingredient");

            migrationBuilder.RenameTable(
                name: "owned_ingredient",
                newName: "OwnedIngredient");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OwnedIngredient",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ingredient_id",
                table: "OwnedIngredient",
                newName: "IngredientId");

            migrationBuilder.RenameColumn(
                name: "owned_ingredient_id",
                table: "OwnedIngredient",
                newName: "OwnedIngredientId");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "OwnedIngredient",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedIngredient",
                table: "OwnedIngredient",
                column: "OwnedIngredientId");
        }
    }
}
