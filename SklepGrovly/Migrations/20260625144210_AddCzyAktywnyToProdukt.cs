using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepGrovly.Migrations
{
    /// <inheritdoc />
    public partial class AddCzyAktywnyToProdukt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyAktywny",
                table: "Produkt",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyAktywny",
                table: "Produkt");
        }
    }
}
