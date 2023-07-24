using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    // name, score, etc
    public string m_CurrentScoreName;
    public string m_HighScoreName;
    public int m_CurrentScore;
    public int m_HighScore;

    // Awake
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }


    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    [System.Serializable]
    class GameData
    {
        public string currentScoreName;
        public string highScoreName;
        public int currentScore;
        public int highScore;
    }

    // Save game data.
    public void SaveScores()
    {
        GameData data = new GameData();
        data.currentScore = m_CurrentScore;
        data.currentScoreName = m_CurrentScoreName;
        data.highScore = m_HighScore;
        data.highScoreName = m_HighScoreName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/scores.json", json);
    }

    // Load game data.
    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/scores.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            m_CurrentScore = data.currentScore;
            m_CurrentScoreName = data.currentScoreName;
            m_HighScore = data.highScore;
            m_HighScoreName = data.highScoreName;
        }
    }
}
