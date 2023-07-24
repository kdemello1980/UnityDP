using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Shim : MonoBehaviour
{
    public TMP_InputField input;
    public TMP_Text ScoreText;
    public void StartGame()
    {
        DataManager.Instance.m_CurrentScoreName = !string.IsNullOrEmpty(input.text) ? input.text : "Player";
        if (string.IsNullOrEmpty(DataManager.Instance.m_HighScoreName))
        {
            DataManager.Instance.m_HighScoreName = "Player";
        }
        ScoreText.text = $"High Score: {DataManager.Instance.m_HighScoreName}: {DataManager.Instance.m_HighScore}";
        SceneManager.LoadScene(1);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
