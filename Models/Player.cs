using System.ComponentModel.DataAnnotations;

namespace BettingGameAPI.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        [Required]
        public string Username { get; set; }

        public int AccountBalance { get; set; } = 10000;
    }
}
