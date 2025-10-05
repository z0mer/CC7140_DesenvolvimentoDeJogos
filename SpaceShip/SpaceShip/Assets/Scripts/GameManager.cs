using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic; // Necessário para usar Listas (List<T>)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public int score = 0;
    public int playerLives = 3;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;

    [Header("UI Vidas (Corações)")]
    public GameObject heartPrefab;
    public Transform heartsContainer;
    private List<GameObject> heartsList = new List<GameObject>();

    [Header("Respawn")]
    public GameObject playerPrefab;
    public Transform respawnPoint;
    public float respawnDelay = 2f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        DrawHearts();
        UpdateScoreUI();
    }

    void DrawHearts()
    {
        foreach (GameObject heart in heartsList)
        {
            Destroy(heart);
        }
        heartsList.Clear();

        for (int i = 0; i < playerLives; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            heartsList.Add(newHeart);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();

        if (score > 0 && score % 100 == 0)
        {
            StartCoroutine(SlowMotionEffect(3f));
        }
    }

    public void PlayerTakesDamage()
    {
        if (playerLives > 0)
        {
            playerLives--;

            if (heartsList.Count > 0)
            {
                GameObject heartToRemove = heartsList[heartsList.Count - 1];
                heartsList.RemoveAt(heartsList.Count - 1);
                Destroy(heartToRemove);
            }

            if (playerLives > 0)
            {
                StartCoroutine(RespawnPlayer());
            }
            else
            {
                GameOver();
            }
        }
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public System.Collections.IEnumerator SlowMotionEffect(float duration)
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void GameOver()
    {
        Invoke("LoadDefeatScene", 1.5f);
    }

    void LoadDefeatScene()
    {
        SceneManager.LoadScene("DefeatScene");
    }

    public void GameWin()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}