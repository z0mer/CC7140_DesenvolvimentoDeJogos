using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; // Não esquece desse

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    
    // ---- A LINHA NOVA QUE A GENTE ADICIONOU ----
    public GameObject playerPortrait; // (Pra gente "plugar" a imagem do Wall-E)
    
    private int index;

    // ---- MÉTODO START ATUALIZADO ----
    void Start()
    {
        textComponent.text = string.Empty;
        
        // Garante que o retrato apareça quando o diálogo começar
        if(playerPortrait != null)
        {
            playerPortrait.SetActive(true); 
        }

        StartDialog();
    }

    void Update()
    {
        // Checa se o mouse existe antes de tentar ler
        if (Mouse.current == null)
        {
            return;
        }

        // Usa o clique do mouse pra avançar
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            if (textComponent.text == lines [index]) {
                NextLine();
            } else {
                // Se clicar no meio, pula a animação e mostra tudo
                StopAllCoroutines();
                textComponent.text = lines [index];
            }
        }
    }

    void StartDialog() {
        index = 0; 
        StartCoroutine (TypeLine());
    }

    IEnumerator TypeLine() {
        foreach (char c in lines [index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds (textSpeed);
        }
    } 

    // ---- MÉTODO NEXTLINE ATUALIZADO ----
    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        } else {
            // Esconde o retrato junto com a caixa
            if(playerPortrait != null)
            {
                playerPortrait.SetActive(false); 
            }
            
            // Esconde a caixa de diálogo
            gameObject.SetActive(false); 
        }
    }
}