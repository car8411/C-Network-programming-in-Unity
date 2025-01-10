using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class GameDbContext : DbContext
{
    public DbSet<PlayerData> Players { get; set; }
    public DbSet<GameData> GameData { get; set; }


    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer("Server=localhost;Database=GameDB;User Id=your_user;Password=your_password;"); // 연결 문자열
        }
    }
}
