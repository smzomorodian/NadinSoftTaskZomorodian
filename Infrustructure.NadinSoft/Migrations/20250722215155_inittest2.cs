using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrustructure.NadinSoft.Migrations
{
    /// <inheritdoc />
    public partial class inittest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_AspNetUsers_ApplicationUserId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_ApplicationUserId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_ApplicationUserId",
                table: "products",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_AspNetUsers_ApplicationUserId",
                table: "products",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
