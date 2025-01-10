using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class SqlService
{
    private readonly GameDbContext _context; // 필드 이름

    public SqlService(GameDbContext context)
    {
        _context = context; // 생성자에서 필드 초기화
    }

    public async Task<List<PlayerData>> GetAllPlayerDataAsync()
    {
        return await _context.Players.ToListAsync(); // _context로 수정
    }

    public async Task<PlayerData> GetPlayerDataByIdAsync(int playerId)
    {
        return await _context.Players.FindAsync(playerId); // _context로 수정
    }

    public async Task CreatePlayerDataAsync(PlayerData playerData)
    {
        _context.Players.Add(playerData); // _context로 수정
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePlayerDataAsync(int playerId, PlayerData updatedPlayerData)
    {
        var player = await _context.Players.FindAsync(playerId); // _context로 수정
        if (player != null)
        {
            player.PlayerName = updatedPlayerData.PlayerName;
            player.TotalScore = updatedPlayerData.TotalScore;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeletePlayerDataAsync(int playerId)
    {
        var player = await _context.Players.FindAsync(playerId); // _context로 수정
        if (player != null)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
    }
}
