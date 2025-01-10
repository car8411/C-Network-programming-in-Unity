using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MongoService
{
    private readonly IMongoCollection<GameData> _gameDataCollection;

    public MongoService()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("GameDatabase");
        _gameDataCollection = database.GetCollection<GameData>("GameData");
    }

    public async Task<List<GameData>> GetAllGameDataAsync()
    {
        return await _gameDataCollection.Find(_ => true).ToListAsync();
    }

    public async Task<GameData> GetGameDataByIdAsync(string id)
    {
        var objectId = ObjectId.Parse(id); // string -> ObjectId 변환
        return await _gameDataCollection.Find(g => g.Id == objectId.ToString()).FirstOrDefaultAsync();
    }


    public async Task CreateGameDataAsync(GameData gameData)
    {
        if (string.IsNullOrEmpty(gameData.Id))
        {
            gameData.Id = ObjectId.GenerateNewId().ToString(); // MongoDB ObjectId 생성
        }

        await _gameDataCollection.InsertOneAsync(gameData);
    }


    public async Task UpdateGameDataAsync(string id, GameData updatedGameData)
    {
        var objectId = ObjectId.Parse(id); // string -> ObjectId 변환
        updatedGameData.Id = objectId.ToString(); // updatedGameData의 Id도 업데이트
        await _gameDataCollection.ReplaceOneAsync(g => g.Id == objectId.ToString(), updatedGameData);
    }

    public async Task DeleteGameDataAsync(string id)
    {
        var objectId = ObjectId.Parse(id); // string -> ObjectId 변환
        await _gameDataCollection.DeleteOneAsync(g => g.Id == objectId.ToString());
    }

}
