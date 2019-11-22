using Microsoft.EntityFrameworkCore.Migrations;

namespace Z02.Migrations
{
    public partial class AddTitleUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Note_Title",
                schema: "tokarzewski",
                table: "Note",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_Title",
                schema: "tokarzewski",
                table: "Category",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Note_Title",
                schema: "tokarzewski",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Category_Title",
                schema: "tokarzewski",
                table: "Category");
        }
    }
}
