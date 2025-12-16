using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    //Purpose: Handle UI clicks

    public TextMeshProUGUI highScoreText;
    public TMP_InputField usernameInput;
    public GameObject panel;
    public GameObject elementList;

    void Start()
    {
        highScoreText.text = $"Best score: {GameManager.instance.playerName}: {GameManager.instance.score}";
    }

    public void OpenHighScoreBoard()
    {
        panel.SetActive(true);
    }

    public void CloseHighScoreBoard()
    {
        panel.SetActive(false);
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
