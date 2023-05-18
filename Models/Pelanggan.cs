using System.ComponentModel.DataAnnotations;

namespace PenjualanUMKM.Models
{
    public class Pelanggan
    {
        [Key] public int Id { get; set; }
        public string namaPelanggan { get; set; }
        public string email { get; set; }
    }
}
