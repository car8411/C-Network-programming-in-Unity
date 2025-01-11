using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SqlService
{
    private readonly GameDbContext _dbContext;

    public SqlService(GameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PlayerData>> GetAllPlayerDataAsync()
    {
        return await _dbContext.Players.ToListAsync();
    }

    public async Task<PlayerData> GetPlayerDataByIdAsync(int id)
    {
        return await _dbContext.Players.FindAsync(id);
    }

    public async Task CreatePlayerDataAsync(PlayerData playerData)
    {
        await _dbContext.Players.AddAsync(playerData); // AddAsync로 비동기 추가
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePlayerDataAsync(int id, PlayerData updatedPlayer)
    {
        var player = await _dbContext.Players.FindAsync(id);
        if (player != null)
        {
            player.PlayerName = updatedPlayer.PlayerName;
            player.TotalScore = updatedPlayer.TotalScore;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeletePlayerDataAsync(int id)
    {
        var player = await _dbContext.Players.FindAsync(id);
        if (player != null)
        {
            _dbContext.Players.Remove(player);
            await _dbContext.SaveChangesAsync();
        }
    }
}
