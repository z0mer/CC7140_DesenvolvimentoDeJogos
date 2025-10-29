using UnityEngine;
using TMPro;
using UnityEngine.UI; // <-- IMPORTANTE: Precisamos disso pra mexer nas Imagens

public class UIManager : MonoBehaviour
{
    // --- Do placar (já tínhamos) ---
    public TextMeshProUGUI scoreText;

    // ---- NOVO AQUI: Lista dos corações ----
    public Image[] hearts; // (A gente vai arrastar os 3 corações aqui)

    // (Opcional) Sprites de coração cheio e vazio, se você tiver
    // public Sprite heartFull;
    // public Sprite heartEmpty;

    void Update()
    {
        // Atualiza o placar de pontos (já tínhamos)
        if (GameManager.instance != null)
        {
            scoreText.text = "Pontos: " + GameManager.instance.currentScore.ToString();
        }
    }

    // ---- NOVO AQUI: Função que o PlayerHealth vai chamar ----
    public void UpdateHealthUI(int currentLives)
    {
        // Loop por todos os corações da nossa lista
        for (int i = 0; i < hearts.Length; i++)
        {
            // Se o "i" (0, 1, 2) for MENOR que a vida atual...
            if (i < currentLives)
            {
                // ...o coração fica ACESO.
                hearts[i].enabled = true;
                // (Se fosse com sprite: hearts[i].sprite = heartFull;)
            }
            else
            {
                // ...senão, o coração fica APAGADO.
                hearts[i].enabled = false;
                // (Se fosse com sprite: hearts[i].sprite = heartEmpty;)
            }
        }
    }
}