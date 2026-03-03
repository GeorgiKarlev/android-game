using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverPanel;

    // Call this when player dies
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // pause the game
    }

    // Restart the current scene
    public void PlayAgain()
    {
        Time.timeScale = 1; // unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // works in editor
    }

    public GameObject enemyPrefab;
    public float enemySpawnRate = 5f;

    public GameObject coinPrefab;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private float screenHalfWidth;
    private float screenHalfHeight;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, enemySpawnRate);

        screenHalfHeight = Camera.main.orthographicSize;
        screenHalfWidth = screenHalfHeight * Screen.width / Screen.height;

        SpawnCoin();
        UpdateScoreUI();
    }

    public void SpawnCoin()
    {
        float randomX = Random.Range(-screenHalfWidth + 0.5f, screenHalfWidth - 0.5f);
        float randomY = Random.Range(-screenHalfHeight + 0.5f, screenHalfHeight - 0.5f);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }

    public void AddScore()
    {
        score++;
        UpdateScoreUI();
        SpawnCoin();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }


    void SpawnEnemy()
    {
        float randomX = Random.Range(-screenHalfWidth + 0.5f, screenHalfWidth - 0.5f);
        float randomY = Random.Range(-screenHalfHeight + 0.5f, screenHalfHeight - 0.5f);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
