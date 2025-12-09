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

    void Start()
    {
        highScoreText.text = $"Best score: {GameManager.instance.bestUsername}: {GameManager.instance.score}";
    }

    // Create functions for Start and Quit
    public void NewGame()
    {
        GameManager.instance.currentUsername = usernameInput.text;
        if (GameManager.instance.currentUsername != "")
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
