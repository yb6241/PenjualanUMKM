using System.ComponentModel.DataAnnotations;

namespace PenjualanUMKM.Models
{
    public class Penjualan
    {
        [Key] public string noSales { get; set; }
        public DateTime tanggal { get; set; }
        public string idPelanggan { get; set; }
        public decimal totalHarga { get; set; }
        public string jenisPembayaran { get; set; }
    }
}
