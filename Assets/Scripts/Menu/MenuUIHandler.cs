using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using Unity.Mathematics;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    //Purpose: Handle UI clicks
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField usernameInput;

    // Leaderboard releated variables
    [SerializeField] GameObject panel;
    [SerializeField] GameObject recordPrefab;
    [SerializeField] Transform wrapper;
    List<GameObject> recordUIsList = new();
    void Start()
    {
        if (GameManager.instance.record.Count > 0)
        {
            GameManager.Player firstPos = GameManager.instance.record[0];
            bestScoreText.text = $"Best score: {firstPos.playerName}: {firstPos.score}";
        }
        else bestScoreText.text = "(crickets' sound)";
    }

    // Leaderboard handling
    public void OpenLeaderboard()
    {
        panel.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        panel.SetActive(false);
    }

    public void UpdateLeaderboard(List<GameManager.Player> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameManager.Player p = list[i];

            if (p.score > 0)
            {
                if (i >= recordUIsList.Count)
                {
                    var inst = Instantiate(recordPrefab, Vector3.zero, quaternion.identity);
                    inst.transform.SetParent(wrapper);

                    recordUIsList.Add(inst);
                }
                var texts = recordUIsList[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = p.playerName;
                texts[1].text = p.score.ToString();
            }
        }
    }

    void OnEnable()
    {
        GameManager.onLeaderboardChanged += UpdateLeaderboard;
    }

    void OnDisable()
    {
        GameManager.onLeaderboardChanged -= UpdateLeaderboard;
    }

    // Create functions for Start and Quit
    public void NewGame()
    {
        GameManager.instance.playerName = usernameInput.text;
        if (GameManager.instance.playerName != "")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            usernameInput.placeholder.color = Color.red;
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
