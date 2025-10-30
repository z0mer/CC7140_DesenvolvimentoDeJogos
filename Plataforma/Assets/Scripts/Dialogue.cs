using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; // (Nao esquece disso)

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; 
    public string[] lines; // (corrigido)
    public float textSpeed; 
    
    // ---- NOVO AQUI ----
    // O campo para "plugar" a imagem do Wall-E
    public GameObject playerPortrait; 
    
    private int index; 

    // ---- METODO START ATUALIZADO ----
    void Start()
    {
        textComponent.text = string.Empty; 
        
        // Garante que o retrato apareca quando o dialogo comecar
        if(playerPortrait != null)
        {
            playerPortrait.SetActive(true); 
        }

        StartDialog(); 
    }

    void Update()
    {
        // Usa o clique do mouse pra avancar (ja com a correcao do Input System)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) 
        {
            if (textComponent.text == lines [index]) { 
                NextLine(); 
            } else {
                // Se clicar no meio, pula a animacao e mostra tudo
                StopAllCoroutines(); 
                textComponent.text = lines [index]; 
            }
        }
    }

    void StartDialog() { 
        index = 0; // (corrigido)
        StartCoroutine (TypeLine()); 
    }

    IEnumerator TypeLine() { 
        foreach (char c in lines [index].ToCharArray()) { 
            textComponent.text += c; 
            yield return new WaitForSeconds (textSpeed); 
        }
    } 

    // ---- METODO NEXTLINE ATUALIZADO ----
    void NextLine() { 
        if (index < lines.Length - 1) { 
            index++; 
            textComponent.text = string.Empty; 
            StartCoroutine (TypeLine()); 
        } else {
            // ---- NOVO AQUI ----
            // Esconde o retrato junto com a caixa
            if(playerPortrait != null)
            {
                playerPortrait.SetActive(false); 
            }
            
            // Esconde a caixa de dialogo
            gameObject.SetActive(false); 
        }
    }
}