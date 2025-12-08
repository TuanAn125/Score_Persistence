#if UNITY_EDITOR
using TMPro;
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    //Purpose: Handle UI clicks

    public TextMeshProUGUI highScoreText;

    void Start()
    {
        highScoreText.text = "High Score: " + 0;
    }



    public void NewGame()
    {
        SceneManager.LoadScene(1);
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
