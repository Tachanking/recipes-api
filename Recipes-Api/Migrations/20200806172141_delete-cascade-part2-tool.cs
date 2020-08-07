using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes_Api.Migrations
{
    public partial class deletecascadepart2tool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "recipe_tool_recipe_id_fkey",
                table: "recipe_tool");

            migrationBuilder.DropForeignKey(
                name: "recipe_tool_tool_id_fkey",
                table: "recipe_tool");

            migrationBuilder.AddForeignKey(
                name: "recipe_tool_recipe_id_fkey",
                table: "recipe_tool",
                column: "recipe_id",
                principalTable: "recipe",
                principalColumn: "recipe_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "recipe_tool_tool_id_fkey",
                table: "recipe_tool",
                column: "tool_id",
                principalTable: "tool",
                principalColumn: "tool_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "recipe_tool_recipe_id_fkey",
                table: "recipe_tool");

            migrationBuilder.DropForeignKey(
                name: "recipe_tool_tool_id_fkey",
                table: "recipe_tool");

            migrationBuilder.AddForeignKey(
                name: "recipe_tool_recipe_id_fkey",
                table: "recipe_tool",
                column: "recipe_id",
                principalTable: "recipe",
                principalColumn: "recipe_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "recipe_tool_tool_id_fkey",
                table: "recipe_tool",
                column: "tool_id",
                principalTable: "tool",
                principalColumn: "tool_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
