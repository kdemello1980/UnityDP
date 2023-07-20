using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    // UI Elements and controls
    public Button startButton;
    public Button quitButton;
    public TMP_InputField nameInput;
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    // UI Elements to disable/enable
    public List<GameObject> MenuElements;
    public List<GameObject> GameElements;
    public List<GameObject> GameOverElements;

    // Start is called before the first frame update
    void Start()
    {
        DisplayGameMenu();
    }

    // Start the actual game
    public void StartGame()
    {
        ShowGame();
        // MainManager.GameStarted = true;
        // const float step = -1.6f;
        // int perLine = Mathf.FloorToInt(3.0f / step);
        
        // int[] pointCountArray = new [] {0,1,2,2,5,5};
        // for (int i = -1; i < LineCount; ++i)
        // {
        //     for (int x = -1; x < perLine; ++x)
        //     {
        //         Vector2 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
        //         Debug.Log(position);
        //         var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
        //         brick.PointValue = pointCountArray[i];
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.GameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MainManager.GameStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }

        }
        else if (!MainManager.GameStarted)
        {
            const float step = -1.6f;
            int perLine = Mathf.FloorToInt(3.0f / step);
            
            int[] pointCountArray = new [] {0,1,2,2,5,5};
            for (int i = -1; i < LineCount; ++i)
            {
                for (int x = -1; x < perLine; ++x)
                {
                    Vector2 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    Debug.Log(position);
                    var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                    brick.PointValue = pointCountArray[i];
                }
            }
            MainManager.GameStarted = true;
        }
        else if (MainManager.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    // Activate/Deactivate GameObjects to show the main menu
    void DisplayGameMenu()
    {
        Debug.Log(MenuElements.Count);
        foreach (var item in MenuElements)
        {
            item.SetActive(true);
        } 

        foreach (var item in GameElements)
        {
            item.SetActive(false);
        } 

        foreach (var item in GameOverElements)
        {
            item.SetActive(false);
        } 
    }

    // Activate/Deactivate GameObjects to show the game
    public void ShowGame()
    {
        foreach (var item in MenuElements)
        {
            item.SetActive(false);
        } 

        foreach (var item in GameElements)
        {
            item.SetActive(true);
        } 

        foreach (var item in GameOverElements)
        {
            item.SetActive(false);
        } 
        StartGame();
    }

    // Launch the game
    public void GameLaunch()
    {
        DisplayGameMenu();
        MainManager.LoadSession();
    }

    // Quit the game
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
