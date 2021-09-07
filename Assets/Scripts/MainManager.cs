using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;

    public InputField nameInput;
    public string nameOfPlayer = "<blank>";
    private bool m_Started = false;
    private int m_Points;
    public int highScore = 0;
    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        nameInput.onEndEdit.AddListener(SubmitName);
        HighScoreText.text = "High Score: " + GameManager.Instance.NameOfPlayer + " : " + GameManager.Instance.HighScore;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
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
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(buildIndex: 0))
        {
            SceneManager.LoadScene(0);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        if (m_Points > highScore)
        {

            highScore = m_Points;
            GameManager.Instance.HighScore = m_Points;
            GameManager.Instance.SaveHighScore();
        }

        m_GameOver = true;
        GameOverText.SetActive(true);
    }
    public void SubmitName(string name)
    {
        nameOfPlayer = name;
        GameManager.Instance.NameOfPlayer = nameOfPlayer;
    }
}
