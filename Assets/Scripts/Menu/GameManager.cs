using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    List<Player> records = new List<Player>();
    int maxCount = 5;

    // When the highscore board is finished, single playerName and score is not needed anymore
    // I'll find a way to remove it
    public string playerName = "";
    public int score;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Load();

        // foreach (Player el in records)
        // {
        //     Debug.Log(el.playerName);
        //     Debug.Log(el.score);
        // }
    }
    [System.Serializable]
    class Player
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
        }
    }
}
