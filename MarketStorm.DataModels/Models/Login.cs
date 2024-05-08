using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MarketStrom.DataModels.Models
{
    public class Login
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
