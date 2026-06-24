using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepGrovly.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoria",
                columns: table => new
                {
                    Id_Kategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoria", x => x.Id_Kategoria);
                });

            migrationBuilder.CreateTable(
                name: "Osoba",
                columns: table => new
                {
                    Id_Osoba = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrTelefonu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataUrodzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypOsoby = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoba", x => x.Id_Osoba);
                });

            migrationBuilder.CreateTable(
                name: "Produkt",
                columns: table => new
                {
                    Id_Produkt = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Znizka = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    IloscNaStanie = table.Column<int>(type: "int", nullable: true),
                    Id_Kategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkt", x => x.Id_Produkt);
                    table.ForeignKey(
                        name: "FK_Produkt_Kategoria_Id_Kategoria",
                        column: x => x.Id_Kategoria,
                        principalTable: "Kategoria",
                        principalColumn: "Id_Kategoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Koszyk",
                columns: table => new
                {
                    Id_Koszyk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Klient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koszyk", x => x.Id_Koszyk);
                    table.ForeignKey(
                        name: "FK_Koszyk_Osoba_Id_Klient",
                        column: x => x.Id_Klient,
                        principalTable: "Osoba",
                        principalColumn: "Id_Osoba",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienie",
                columns: table => new
                {
                    Id_Zamowienie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataZamowienia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_Klient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienie", x => x.Id_Zamowienie);
                    table.ForeignKey(
                        name: "FK_Zamowienie_Osoba_Id_Klient",
                        column: x => x.Id_Klient,
                        principalTable: "Osoba",
                        principalColumn: "Id_Osoba",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Opinia",
                columns: table => new
                {
                    Id_Opinia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocena = table.Column<int>(type: "int", nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DataWystawienia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Klient = table.Column<int>(type: "int", nullable: false),
                    Id_Produkt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinia", x => x.Id_Opinia);
                    table.ForeignKey(
                        name: "FK_Opinia_Osoba_Id_Klient",
                        column: x => x.Id_Klient,
                        principalTable: "Osoba",
                        principalColumn: "Id_Osoba",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opinia_Produkt_Id_Produkt",
                        column: x => x.Id_Produkt,
                        principalTable: "Produkt",
                        principalColumn: "Id_Produkt",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PozycjaWKoszyku",
                columns: table => new
                {
                    Id_Pozycja_Koszyk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilosc = table.Column<int>(type: "int", nullable: false),
                    Id_Koszyk = table.Column<int>(type: "int", nullable: false),
                    Id_Produkt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjaWKoszyku", x => x.Id_Pozycja_Koszyk);
                    table.ForeignKey(
                        name: "FK_PozycjaWKoszyku_Koszyk_Id_Koszyk",
                        column: x => x.Id_Koszyk,
                        principalTable: "Koszyk",
                        principalColumn: "Id_Koszyk",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PozycjaWKoszyku_Produkt_Id_Produkt",
                        column: x => x.Id_Produkt,
                        principalTable: "Produkt",
                        principalColumn: "Id_Produkt",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Platnosc",
                columns: table => new
                {
                    Id_Platnosc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KwotaPlatnosci = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataPlatnosci = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodaPlatnosci = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusPlatnosci = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdZBramkiPlatniczej = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Id_Zamowienie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platnosc", x => x.Id_Platnosc);
                    table.ForeignKey(
                        name: "FK_Platnosc_Zamowienie_Id_Zamowienie",
                        column: x => x.Id_Zamowienie,
                        principalTable: "Zamowienie",
                        principalColumn: "Id_Zamowienie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PozycjaWZamowieniu",
                columns: table => new
                {
                    Id_Pozycja_Zamowienie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilosc = table.Column<int>(type: "int", nullable: false),
                    CenaZakupu = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Id_Zamowienie = table.Column<int>(type: "int", nullable: false),
                    Id_Produkt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjaWZamowieniu", x => x.Id_Pozycja_Zamowienie);
                    table.ForeignKey(
                        name: "FK_PozycjaWZamowieniu_Produkt_Id_Produkt",
                        column: x => x.Id_Produkt,
                        principalTable: "Produkt",
                        principalColumn: "Id_Produkt",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PozycjaWZamowieniu_Zamowienie_Id_Zamowienie",
                        column: x => x.Id_Zamowienie,
                        principalTable: "Zamowienie",
                        principalColumn: "Id_Zamowienie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Koszyk_Id_Klient",
                table: "Koszyk",
                column: "Id_Klient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opinia_Id_Klient_Id_Produkt",
                table: "Opinia",
                columns: new[] { "Id_Klient", "Id_Produkt" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opinia_Id_Produkt",
                table: "Opinia",
                column: "Id_Produkt");

            migrationBuilder.CreateIndex(
                name: "IX_Platnosc_Id_Zamowienie",
                table: "Platnosc",
                column: "Id_Zamowienie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaWKoszyku_Id_Koszyk",
                table: "PozycjaWKoszyku",
                column: "Id_Koszyk");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaWKoszyku_Id_Produkt",
                table: "PozycjaWKoszyku",
                column: "Id_Produkt");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaWZamowieniu_Id_Produkt",
                table: "PozycjaWZamowieniu",
                column: "Id_Produkt");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaWZamowieniu_Id_Zamowienie",
                table: "PozycjaWZamowieniu",
                column: "Id_Zamowienie");

            migrationBuilder.CreateIndex(
                name: "IX_Produkt_Id_Kategoria",
                table: "Produkt",
                column: "Id_Kategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienie_Id_Klient",
                table: "Zamowienie",
                column: "Id_Klient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opinia");

            migrationBuilder.DropTable(
                name: "Platnosc");

            migrationBuilder.DropTable(
                name: "PozycjaWKoszyku");

            migrationBuilder.DropTable(
                name: "PozycjaWZamowieniu");

            migrationBuilder.DropTable(
                name: "Koszyk");

            migrationBuilder.DropTable(
                name: "Produkt");

            migrationBuilder.DropTable(
                name: "Zamowienie");

            migrationBuilder.DropTable(
                name: "Kategoria");

            migrationBuilder.DropTable(
                name: "Osoba");
        }
    }
}
