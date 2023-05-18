using System.ComponentModel.DataAnnotations;

namespace PenjualanUMKM.Models
{
    public class Transaksi
    {
        [Key] public int Id { get; set; }
        public string idSales { get; set; }
        public string idProduk { get; set; }
        public int qty { get; set; }
    }
}
