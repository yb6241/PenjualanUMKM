using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PenjualanUMKM.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Antrians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    noAntrian = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antrians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogUMKMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noTiket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    judul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    keterangan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogUMKMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pelanggans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    namaPelanggan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelanggans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Penjualans",
                columns: table => new
                {
                    noSales = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tanggal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idPelanggan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalHarga = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    jenisPembayaran = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penjualans", x => x.noSales);
                });

            migrationBuilder.CreateTable(
                name: "Produks",
                columns: table => new
                {
                    kodeProduk = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    namaProduk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hargaProduk = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produks", x => x.kodeProduk);
                });

            migrationBuilder.CreateTable(
                name: "Transaksis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idProduk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksis", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Antrians");

            migrationBuilder.DropTable(
                name: "LogUMKMs");

            migrationBuilder.DropTable(
                name: "Pelanggans");

            migrationBuilder.DropTable(
                name: "Penjualans");

            migrationBuilder.DropTable(
                name: "Produks");

            migrationBuilder.DropTable(
                name: "Transaksis");
        }
    }
}
