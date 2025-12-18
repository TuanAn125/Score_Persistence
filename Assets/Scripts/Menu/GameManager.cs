using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] List<Player> records = new();
    [SerializeField] int maxCount = 5;

    public string playerName = "";

    public delegate void OnLeaderboardChanged(List<Player> list);
    public static event OnLeaderboardChanged onLeaderboardChanged;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    // Make fetching data from the list available
    public IReadOnlyList<Player> record => records;

    [System.Serializable]
    public class Player
    {
        public string playerName;
        public int score;

        public Player(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }

    [System.Serializable]
    class Records
    {
        public List<Player> records;
    }

    public void Save(string playerName, int score)
    {
        Records data = new();
        data.records = records;
        data.records.Add(new(playerName, score));
        data.records.Sort((a, b) =>
        {
            if (b.score == a.score)
                return a.playerName.CompareTo(b.playerName);
            else return b.score.CompareTo(a.score);
        });

        while (records.Count > maxCount)
        {
            records.RemoveAt(maxCount);
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/record.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/record.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Records data = JsonUtility.FromJson<Records>(json);
            records = data.records;

            //Onchange != null, then trigger
            onLeaderboardChanged?.Invoke(records);
        }
    }
}
