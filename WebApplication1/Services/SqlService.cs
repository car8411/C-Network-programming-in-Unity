using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

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

    public async Task<PlayerData> GetPlayerDataByIdAsync(int playerId)
    {
        return await _dbContext.Players.FindAsync(playerId);
    }

    public async Task CreatePlayerDataAsync(PlayerData playerData)
    {
        _dbContext.Players.Add(playerData);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePlayerDataAsync(int playerId, PlayerData updatedPlayerData)
    {
        var player = await _dbContext.Players.FindAsync(playerId);
        if (player != null)
        {
            player.PlayerName = updatedPlayerData.PlayerName;
            player.TotalScore = updatedPlayerData.TotalScore;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeletePlayerDataAsync(int playerId)
    {
        var player = await _dbContext.Players.FindAsync(playerId);
        if (player != null)
        {
            _dbContext.Players.Remove(player);
            await _dbContext.SaveChangesAsync();
        }
    }
}
