using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes_Api.Migrations
{
    public partial class removedIsOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_optional",
                table: "recipe_ingredient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_optional",
                table: "recipe_ingredient",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
