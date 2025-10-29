using UnityEngine;
using UnityEngine.SceneManagement; // <-- IMPORTANTE: Precisamos disso!

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentScore = 0;
    
    // ---- NOVO AQUI: O "Flag" de vitória ----
    private bool hasWon = false; // Impede que o jogo tente carregar a cena 500x
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int pointsToAdd)
    {
        // Se a gente já ganhou, nem soma mais
        if (hasWon) return; 

        currentScore += pointsToAdd;
        Debug.Log("Pontos atuais: " + currentScore); 
        
        // ---- A MÁGICA ACONTECE AQUI ----
        // Se a pontuação for 50 OU MAIS, e a gente ainda não ganhou...
        if (currentScore >= 50 && !hasWon)
        {
            // 1. Marca que a gente ganhou
            hasWon = true; 
            
            // 2. Carrega a cena de vitória!
            Debug.Log("VITÓRIA! Carregando WinScene...");
            SceneManager.LoadScene("WinScene");
        }
    }
}