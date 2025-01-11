using Microsoft.EntityFrameworkCore;
using WebApplication1.Models; // PlayerData를 참조하기 위한 네임스페이스 추가

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
    }

    public DbSet<PlayerData> Players { get; set; } // WebApplication1.Models.PlayerData를 사용
       }
