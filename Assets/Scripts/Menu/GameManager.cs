using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Purpose: Manage storable data such as names and highscores

    public static GameManager instance;
    public string username = "";
    public int score = 0;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadRecords();
    }

    [System.Serializable]
    class SaveData
    {
        public string s_username;
        public int s_score;
    }

    // Ok, I'm going to make a save/load data methods here
    public void SaveRecords()
    {
        SaveData data = new SaveData();
        data.s_username = username;
        data.s_score = score;

        // Data is created. It'll be saved in a json file
        // To do that, I need methods from JsonUtility
        string json = JsonUtility.ToJson(data);
        // As it is output as file, so I call File
        // The saved file's name is record.json
        File.WriteAllText(Application.persistentDataPath + "/record.json", json);
    }

    public void LoadRecords()
    {
        // Guess I'll need to create empty string to handle the loaded data
        // I need a path first
        string path = Application.persistentDataPath + "/record.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            username = data.s_username;
            score = data.s_score;
        }
    }
}
