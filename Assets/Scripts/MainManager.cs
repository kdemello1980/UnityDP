using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    private bool m_GameSaved = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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

        DataManager.Instance.m_CurrentScore = 0;
        m_GameSaved = false;
        HighScoreText.text = $"High Score: {DataManager.Instance.m_HighScoreName} : {DataManager.Instance.m_HighScore}";
        ScoreText.text = $"Score : {DataManager.Instance.m_CurrentScoreName} : {DataManager.Instance.m_CurrentScore}";
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
            if (!m_GameSaved)
            {
                DataManager.Instance.SaveScores();
                m_GameSaved = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        Debug.Log("adding point");
        DataManager.Instance.m_CurrentScore += point;
        ScoreText.text = $"Score : {DataManager.Instance.m_CurrentScoreName} : {DataManager.Instance.m_CurrentScore}";

        // compare with the high score and update score/name accordingly
        if (DataManager.Instance.m_CurrentScore > DataManager.Instance.m_HighScore) 
        {
            DataManager.Instance.m_HighScore = DataManager.Instance.m_CurrentScore;
            DataManager.Instance.m_HighScoreName = DataManager.Instance.m_CurrentScoreName;
        }

        HighScoreText.text = $"High Score: {DataManager.Instance.m_HighScoreName} : {DataManager.Instance.m_HighScore}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        DataManager.Instance.SaveScores();
        GameOverText.SetActive(true);
    }
    
    public void QuitGame()
    {
        GameOver();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
