using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class GameData
    {
        [BsonId] // MongoDB의 기본 키
        [BsonRepresentation(BsonType.ObjectId)] // ObjectId로 매핑
        public string? Id { get; set; } // 여기는 반드시 string

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Score")]
        public int Score { get; set; }
    }
}
