using System.ComponentModel.DataAnnotations;

namespace PenjualanUMKM.Models
{
    public class Produk
    {
        [Key] public string kodeProduk { get; set; }
        public string namaProduk { get; set; }
        public decimal hargaProduk { get; set; }
    }
}
