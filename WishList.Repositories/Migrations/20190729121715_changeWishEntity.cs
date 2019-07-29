using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WishList.Repositories.Migrations
{
    public partial class changeWishEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishes",
                table: "Wishes");

            migrationBuilder.DropIndex(
                name: "IX_Wishes_UserId",
                table: "Wishes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wishes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishes",
                table: "Wishes",
                columns: new[] { "UserId", "ProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishes",
                table: "Wishes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wishes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishes",
                table: "Wishes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wishes_UserId",
                table: "Wishes",
                column: "UserId");
        }
    }
}
