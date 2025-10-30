using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    // Funcao que o botao "Tentar Novamente" vai chamar
    public void RestartGame()
    {
        // Carrega a Fase 1 de novo
        SceneManager.LoadScene("SampleScene");
    }

    // (Opcional) Botao pra voltar pro menu
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}