using System.ComponentModel.DataAnnotations;

namespace PenjualanUMKM.Models
{
    public class Antrian
    {
        [Key] public int Id { get; set; }
        public string idSales { get; set; }
        public string noAntrian { get; set; }
        public string status { get; set; }
    }
}
