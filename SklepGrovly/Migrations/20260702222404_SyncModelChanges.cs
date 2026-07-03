using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepGrovly.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaWKoszyku_Koszyk_Id_Koszyk",
                table: "PozycjaWKoszyku");

            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaWKoszyku_Produkt_Id_Produkt",
                table: "PozycjaWKoszyku");

            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaWZamowieniu_Produkt_Id_Produkt",
                table: "PozycjaWZamowieniu");

            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaWZamowieniu_Zamowienie_Id_Zamowienie",
                table: "PozycjaWZamowieniu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PozycjaWZamowieniu",
                table: "PozycjaWZamowieniu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PozycjaWKoszyku",
                table: "PozycjaWKoszyku");

            migrationBuilder.RenameTable(
                name: "PozycjaWZamowieniu",
                newName: "PozycjeWZamowieniu");

            migrationBuilder.RenameTable(
                name: "PozycjaWKoszyku",
                newName: "PozycjeWKoszyku");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjaWZamowieniu_Id_Zamowienie",
                table: "PozycjeWZamowieniu",
                newName: "IX_PozycjeWZamowieniu_Id_Zamowienie");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjaWZamowieniu_Id_Produkt",
                table: "PozycjeWZamowieniu",
                newName: "IX_PozycjeWZamowieniu_Id_Produkt");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjaWKoszyku_Id_Produkt",
                table: "PozycjeWKoszyku",
                newName: "IX_PozycjeWKoszyku_Id_Produkt");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjaWKoszyku_Id_Koszyk",
                table: "PozycjeWKoszyku",
                newName: "IX_PozycjeWKoszyku_Id_Koszyk");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PozycjeWZamowieniu",
                table: "PozycjeWZamowieniu",
                column: "Id_Pozycja_Zamowienie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PozycjeWKoszyku",
                table: "PozycjeWKoszyku",
                column: "Id_Pozycja_Koszyk");

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjeWKoszyku_Koszyk_Id_Koszyk",
                table: "PozycjeWKoszyku",
                column: "Id_Koszyk",
                principalTable: "Koszyk",
                principalColumn: "Id_Koszyk",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjeWKoszyku_Produkt_Id_Produkt",
                table: "PozycjeWKoszyku",
                column: "Id_Produkt",
                principalTable: "Produkt",
                principalColumn: "Id_Produkt",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjeWZamowieniu_Produkt_Id_Produkt",
                table: "PozycjeWZamowieniu",
                column: "Id_Produkt",
                principalTable: "Produkt",
                principalColumn: "Id_Produkt",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjeWZamowieniu_Zamowienie_Id_Zamowienie",
                table: "PozycjeWZamowieniu",
                column: "Id_Zamowienie",
                principalTable: "Zamowienie",
                principalColumn: "Id_Zamowienie",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PozycjeWKoszyku_Koszyk_Id_Koszyk",
                table: "PozycjeWKoszyku");

            migrationBuilder.DropForeignKey(
                name: "FK_PozycjeWKoszyku_Produkt_Id_Produkt",
                table: "PozycjeWKoszyku");

            migrationBuilder.DropForeignKey(
                name: "FK_PozycjeWZamowieniu_Produkt_Id_Produkt",
                table: "PozycjeWZamowieniu");

            migrationBuilder.DropForeignKey(
                name: "FK_PozycjeWZamowieniu_Zamowienie_Id_Zamowienie",
                table: "PozycjeWZamowieniu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PozycjeWZamowieniu",
                table: "PozycjeWZamowieniu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PozycjeWKoszyku",
                table: "PozycjeWKoszyku");

            migrationBuilder.RenameTable(
                name: "PozycjeWZamowieniu",
                newName: "PozycjaWZamowieniu");

            migrationBuilder.RenameTable(
                name: "PozycjeWKoszyku",
                newName: "PozycjaWKoszyku");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjeWZamowieniu_Id_Zamowienie",
                table: "PozycjaWZamowieniu",
                newName: "IX_PozycjaWZamowieniu_Id_Zamowienie");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjeWZamowieniu_Id_Produkt",
                table: "PozycjaWZamowieniu",
                newName: "IX_PozycjaWZamowieniu_Id_Produkt");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjeWKoszyku_Id_Produkt",
                table: "PozycjaWKoszyku",
                newName: "IX_PozycjaWKoszyku_Id_Produkt");

            migrationBuilder.RenameIndex(
                name: "IX_PozycjeWKoszyku_Id_Koszyk",
                table: "PozycjaWKoszyku",
                newName: "IX_PozycjaWKoszyku_Id_Koszyk");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PozycjaWZamowieniu",
                table: "PozycjaWZamowieniu",
                column: "Id_Pozycja_Zamowienie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PozycjaWKoszyku",
                table: "PozycjaWKoszyku",
                column: "Id_Pozycja_Koszyk");

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaWKoszyku_Koszyk_Id_Koszyk",
                table: "PozycjaWKoszyku",
                column: "Id_Koszyk",
                principalTable: "Koszyk",
                principalColumn: "Id_Koszyk",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaWKoszyku_Produkt_Id_Produkt",
                table: "PozycjaWKoszyku",
                column: "Id_Produkt",
                principalTable: "Produkt",
                principalColumn: "Id_Produkt",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaWZamowieniu_Produkt_Id_Produkt",
                table: "PozycjaWZamowieniu",
                column: "Id_Produkt",
                principalTable: "Produkt",
                principalColumn: "Id_Produkt",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaWZamowieniu_Zamowienie_Id_Zamowienie",
                table: "PozycjaWZamowieniu",
                column: "Id_Zamowienie",
                principalTable: "Zamowienie",
                principalColumn: "Id_Zamowienie",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
