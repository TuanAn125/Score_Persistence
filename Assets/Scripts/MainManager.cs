
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private string m_Player = GameManager.instance.playerName;
    private int m_Points;
    private bool m_GameOver = false;
    private int remaining;

    void Start()
    {
        UpdateTop();
        AddBricks();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (remaining == 0)
        {
            AddBricks();
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void UpdateTop()
    {
        if (GameManager.instance.record.Count > 0)
        {
            GameManager.Player firstPos = GameManager.instance.record[0];
            bestScoreText.text = $"Best score: {firstPos.playerName}: {firstPos.score}";
        }
        else bestScoreText.text = "There is no one here";
    }

    void AddBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        remaining = LineCount * perLine;

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                brick.onDestroyed.AddListener(_ => remaining--);
            }
        }
    }

    public void GameOver()
    {
        GameManager.instance.Save(m_Player, m_Points);
        m_GameOver = true;
        GameOverText.SetActive(true);
        UpdateTop();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
