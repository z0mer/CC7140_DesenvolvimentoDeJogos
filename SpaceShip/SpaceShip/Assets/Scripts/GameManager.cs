// GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement; // Para mudar de cena
using TMPro; // Para usar o TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton

    public int score = 0;
    public TextMeshProUGUI scoreText; // Arraste o texto da UI aqui

    void Awake()
    {
        // Lógica do Singleton para garantir que só existe um GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;

        // Gatilho para o slow-motion (exemplo: a cada 100 pontos)
        if (score > 0 && score % 100 == 0)
        {
            StartCoroutine(SlowMotionEffect(3f)); // Ativa por 3 segundos
        }
    }
    
    // --- FUNÇÃO PARA DESACELERAR O TEMPO ---
    public System.Collections.IEnumerator SlowMotionEffect(float duration)
    {
        Time.timeScale = 0.5f; // Deixa o tempo 50% mais lento
        // Importante para a física não bugar em slow motion
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(duration); // Espera o tempo real

        Time.timeScale = 1f; // Volta ao normal
        Time.fixedDeltaTime = 0.02f;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("DefeatScene");
    }

    public void GameWin()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}