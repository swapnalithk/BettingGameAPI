using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BettingGameAPI.Models
{
    public class Bet
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public int PlayerId { get; set; }  

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }  

        [Required]
        public int Points { get; set; }

        [Required]
        public int PredictedNumber { get; set; }

        public int GeneratedNumber { get; set; }

        public bool IsWin { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}