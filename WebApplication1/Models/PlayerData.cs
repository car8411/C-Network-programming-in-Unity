using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PlayerData
    {
        [Key]
        public int PlayerId { get; set; } // 기본 키 설정

        public string PlayerName { get; set; }
        public int TotalScore { get; set; }
    }
}
