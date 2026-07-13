using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepGrovly.Migrations
{
    /// <inheritdoc />
    public partial class AddGuestAndOsobaOrderRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zamowienie_Osoba_Id_Klient",
                table: "Zamowienie");

            migrationBuilder.RenameColumn(
                name: "Id_Klient",
                table: "Zamowienie",
                newName: "Id_Osoba");

            migrationBuilder.RenameIndex(
                name: "IX_Zamowienie_Id_Klient",
                table: "Zamowienie",
                newName: "IX_Zamowienie_Id_Osoba");

            migrationBuilder.AddForeignKey(
                name: "FK_Zamowienie_Osoba_Id_Osoba",
                table: "Zamowienie",
                column: "Id_Osoba",
                principalTable: "Osoba",
                principalColumn: "Id_Osoba",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zamowienie_Osoba_Id_Osoba",
                table: "Zamowienie");

            migrationBuilder.RenameColumn(
                name: "Id_Osoba",
                table: "Zamowienie",
                newName: "Id_Klient");

            migrationBuilder.RenameIndex(
                name: "IX_Zamowienie_Id_Osoba",
                table: "Zamowienie",
                newName: "IX_Zamowienie_Id_Klient");

            migrationBuilder.AddForeignKey(
                name: "FK_Zamowienie_Osoba_Id_Klient",
                table: "Zamowienie",
                column: "Id_Klient",
                principalTable: "Osoba",
                principalColumn: "Id_Osoba",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
