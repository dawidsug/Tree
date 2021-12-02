using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class SeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isNode",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "isOpen",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "isNode",
                table: "Leafs");

            migrationBuilder.DropColumn(
                name: "isOpen",
                table: "Leafs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isNode",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isOpen",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isNode",
                table: "Leafs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isOpen",
                table: "Leafs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
