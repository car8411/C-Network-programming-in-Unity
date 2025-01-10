namespace MyProject.Models
{
    [System.Serializable]
    public class GameData
    {
        public IdWrapper Id; // id�� ���� ��ü�� ����
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
