using System.ComponentModel.DataAnnotations;

namespace PenjualanUMKM.Models
{
    public class LogUMKM
    {
        [Key] public int Id { get; set; }
        public string noTiket { get; set; }
        public string judul { get; set; }
        public string keterangan { get; set; }
    }
}
