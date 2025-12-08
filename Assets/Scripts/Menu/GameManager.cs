using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Purpose: Manage storable data such as names and highscores

    public static GameManager instance;
    public string playerName;
    public int highScore;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    class SaveData
    {
        public string playerName;
        public int highScore;
    }

    // Ok, I'm going to make a save/load data methods here
}
