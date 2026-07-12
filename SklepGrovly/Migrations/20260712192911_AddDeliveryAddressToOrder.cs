using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepGrovly.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryAddressToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImieOdbiorcy",
                table: "Zamowienie",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KodPocztowy",
                table: "Zamowienie",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Miejscowosc",
                table: "Zamowienie",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NazwiskoOdbiorcy",
                table: "Zamowienie",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NrDomu",
                table: "Zamowienie",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelefonOdbiorcy",
                table: "Zamowienie",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ulica",
                table: "Zamowienie",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImieOdbiorcy",
                table: "Zamowienie");

            migrationBuilder.DropColumn(
                name: "KodPocztowy",
                table: "Zamowienie");

            migrationBuilder.DropColumn(
                name: "Miejscowosc",
                table: "Zamowienie");

            migrationBuilder.DropColumn(
                name: "NazwiskoOdbiorcy",
                table: "Zamowienie");

            migrationBuilder.DropColumn(
                name: "NrDomu",
                table: "Zamowienie");

            migrationBuilder.DropColumn(
                name: "TelefonOdbiorcy",
                table: "Zamowienie");

            migrationBuilder.DropColumn(
                name: "Ulica",
                table: "Zamowienie");
        }
    }
}
