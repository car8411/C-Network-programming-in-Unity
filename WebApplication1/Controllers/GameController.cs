using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly MongoService _mongoService;
    private readonly SqlService _sqlService;

    public GameController(MongoService mongoService, SqlService sqlService)
    {
        _mongoService = mongoService;
        _sqlService = sqlService;
    }

    // 기존 MongoDB API
    [HttpGet]
    public async Task<IActionResult> GetAllGameData()
    {
        var gameDataList = await _mongoService.GetAllGameDataAsync();
        return Ok(gameDataList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameDataById(string id)
    {
        var gameData = await _mongoService.GetGameDataByIdAsync(id);
        if (gameData == null) return NotFound();
        return Ok(gameData);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGameData([FromBody] GameData newGameData)
    {
        await _mongoService.CreateGameDataAsync(newGameData);
        return Ok(newGameData);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGameData(string id, [FromBody] GameData updatedGameData)
    {
        await _mongoService.UpdateGameDataAsync(id, updatedGameData);
        return Ok(updatedGameData);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameData(string id)
    {
        await _mongoService.DeleteGameDataAsync(id);
        return Ok();
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard()
    {
        var leaderboard = await _mongoService.GetAllGameDataAsync();
        leaderboard.Sort((x, y) => y.Score.CompareTo(x.Score)); // 점수 내림차순 정렬
        return Ok(leaderboard.Take(10)); // 상위 10명 반환
    }

    // SQL 데이터 관리 API
    [HttpGet("players")]
    public async Task<IActionResult> GetAllPlayers()
    {
        var players = await _sqlService.GetAllPlayerDataAsync();
        return Ok(players);
    }

    [HttpGet("players/{id}")]
    public async Task<IActionResult> GetPlayerById(int id)
    {
        var player = await _sqlService.GetPlayerDataByIdAsync(id);
        if (player == null) return NotFound();
        return Ok(player);
    }

    [HttpPost("players")]
    public async Task<IActionResult> CreatePlayer([FromBody] PlayerData playerData)
    {
        await _sqlService.CreatePlayerDataAsync(playerData);
        return Ok(playerData);
    }

    [HttpPut("players/{id}")]
    public async Task<IActionResult> UpdatePlayer(int id, [FromBody] PlayerData updatedPlayerData)
    {
        await _sqlService.UpdatePlayerDataAsync(id, updatedPlayerData);
        return Ok(updatedPlayerData);
    }

    [HttpDelete("players/{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        await _sqlService.DeletePlayerDataAsync(id);
        return Ok();
    }

    // MongoDB와 SQL 데이터 통합
    [HttpGet("combined")]
    public async Task<IActionResult> GetCombinedData()
    {
        var gameDataList = await _mongoService.GetAllGameDataAsync();
        var playerDataList = await _sqlService.GetAllPlayerDataAsync();

        // MongoDB와 SQL 데이터를 조합
        var combinedData = from game in gameDataList
                           join player in playerDataList on game.Name equals player.PlayerName
                           select new
                           {
                               GameId = game.Id,
                               PlayerId = player.PlayerId,
                               game.Name,
                               GameScore = game.Score,
                               PlayerTotalScore = player.TotalScore
                           };

        return Ok(combinedData.ToList());
    }
}
