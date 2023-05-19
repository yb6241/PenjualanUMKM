using System.ComponentModel.DataAnnotations;

namespace PenjualanUMKM.Models
{
    public class User
    {
        [Key] public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
