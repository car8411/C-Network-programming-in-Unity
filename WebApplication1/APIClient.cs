using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using MyProject.Models;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

public class APIClient : MonoBehaviour
{
    private string baseUrl = "https://localhost:7278/api/Game";
    public TextMeshProUGUI gameDataText; // UI Text to display game data

    void Start()
    {
        StartCoroutine(GetGameData());
    }

    IEnumerator GetGameData()
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log("Received JSON: " + json); // ����׿�

            // JSON �Ľ�
            List<GameData> gameDataList = JsonConvert.DeserializeObject<List<GameData>>(json);

            DisplayGameData(gameDataList);
        }
    }

    private void DisplayGameData(List<GameData> gameDataList)
    {
        if (gameDataText == null)
        {
            Debug.LogError("gameDataText is not assigned in the Inspector!");
            return;
        }

        gameDataText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ

        foreach (var data in gameDataList)
        {
            gameDataText.text += $"Id Timestamp: {data.Id.timestamp}, Name: {data.Name}, Score: {data.Score}\n";
        }
    }
}
