namespace MyProject.Models
{
    [System.Serializable]
    public class GameData
    {
        public IdWrapper Id; // id를 복합 객체로 매핑
        public string Name;
        public int Score;
    }

    [System.Serializable]
    public class IdWrapper
    {
        public int timestamp;
        public string creationTime;
    }
}
