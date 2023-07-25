using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
        SceneManager.LoadScene(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = $"High Score: {DataManager.Instance.m_HighScoreName}: {DataManager.Instance.m_HighScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
