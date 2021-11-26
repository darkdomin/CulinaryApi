using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryApi.Migrations
{
    public partial class AddPropertiesCreatedById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateById",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CreatedById",
                table: "Recipes",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_CreatedById",
                table: "Recipes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_CreatedById",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CreatedById",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Recipes");
        }
    }
}
