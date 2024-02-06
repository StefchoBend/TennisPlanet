using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisPlanet.Infrastructure.Migrations
{
    public partial class ChangeOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "QuantityInStock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityInStock",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
