using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PlayerData
    {
        [Key]
        public int PlayerId { get; set; } // Primary Key
        public string PlayerName { get; set; }
        public int TotalScore { get; set; }
    }
}
