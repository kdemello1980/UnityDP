using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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

    // Start is called before the first frame update
    void Start()
    {
        GameLaunch();
        StartGame();
    }

    // Start the actual game
    void StartGame()
    {
        MainManager.GameStarted = true;
        ShowGame();
        const float step = -1.6f;
        int perLine = Mathf.FloorToInt(3.0f / step);
        
        int[] pointCountArray = new [] {0,1,2,2,5,5};
        for (int i = -1; i < LineCount; ++i)
        {
            for (int x = -1; x < perLine; ++x)
            {
                Vector2 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                // brick.onDestroyed.AddListener(MainManager.AddPoints(brick.PointValue));
                // MainManager.AddPoints(brick.PointValue);
            }
        }
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
        GameObject.Find("DeathZone").gameObject.SetActive(false);
        GameObject.Find("Borders").gameObject.SetActive(false);
        GameObject.Find("Paddle").gameObject.SetActive(false);
        GameObject.Find("GameoverText").gameObject.SetActive(false);

        GameObject.Find("MainMenu").gameObject.SetActive(true);
    }

    // Activate/Deactivate GameObjects to show the game
    void ShowGame()
    {
        GameObject.Find("DeathZone").gameObject.SetActive(true);
        GameObject.Find("Borders").gameObject.SetActive(true);
        GameObject.Find("Paddle").gameObject.SetActive(true);
        GameObject.Find("GameoverText").gameObject.SetActive(true);

        GameObject.Find("MainMenu").gameObject.SetActive(false);
    }

    // Launch the game
    void GameLaunch()
    {
        DisplayGameMenu();
        MainManager.LoadSession();
    }

    // Get player info
    // void GetPlayerInfo()
    // {
    //     CurrentScorePlayer = nameInput.text;
    // }
}
