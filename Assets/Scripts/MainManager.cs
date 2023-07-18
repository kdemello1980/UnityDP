using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static bool GameOver;
    public static bool GameStarted;
    public static int CurrentScore;
    public static int HighScore;
    public static string CurrentScorePlayer;
    public static string HighScorePlayer;

    // Make MainManager a singleton
    public static MainManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSession();
    }

    // Add points to the score. If the resulting point total is greater than
    // the high score, replace the high score data with this user (will update)
    public static void AddPoints(int points)
    {
        CurrentScore += points;
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            HighScorePlayer = CurrentScorePlayer;
        }
    }

    // SaveData class
    [System.Serializable]
    class SaveData
    {
        public int CurrentScore;
        public int HighScore;
        public string CurrentScorePlayer;
        public string HighScorePlayer;
    }
    
    public static void SaveSession()
    {
        SaveData data = new SaveData();

        data.CurrentScore = CurrentScore;
        data.CurrentScorePlayer = CurrentScorePlayer;
        data.HighScore = HighScore;
        data.HighScorePlayer = HighScorePlayer;

        string json = JsonUtility.ToJson(data);


        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Load the data from the file to persist in between sessions.
    public static void LoadSession()
    {    
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            CurrentScore = data.CurrentScore;
            CurrentScorePlayer = data.CurrentScorePlayer;
            HighScore = data.HighScore;
            HighScorePlayer = data.HighScorePlayer;
        }
        GameOver = false;
    }
}
