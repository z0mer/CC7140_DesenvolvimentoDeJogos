// EnemyController.cs
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 4f; // Velocidade que o inimigo desce na tela
    public int health = 1;   // Quantos tiros pra morrer (por enquanto 1)

    // EnemyController.cs (trecho modificado)

    void Update()
    {
        // --- MUDANÇA DE DIREÇÃO ---
        // Trocamos Vector2.down por Vector2.left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // --- MUDANÇA DA AUTO-DESTRUIÇÃO ---
        // Agora checamos se ele saiu da tela pela esquerda (posição X)
        if (transform.position.x < -11f) // Ajuste esse valor se precisar (depende da largura da sua tela)
        {
            Destroy(gameObject);
        }
    }

    // Este método vai detectar colisões com outros Triggers (nosso tiro!)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Se o objeto que colidiu tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Por enquanto, vamos só destruir o player e o inimigo
            // Mais tarde a gente chama a tela de Game Over aqui
            Destroy(other.gameObject);
            Destroy(gameObject); 
            // GameManager.Instance.GameOver(); // Linha pra quando a tela de derrota estiver pronta
            return; // Sai da função pra não executar o código abaixo
        }

        // Se o objeto que colidiu tem a tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Primeiro, destrói o tiro
            Destroy(other.gameObject);
            
            // Reduz a vida do inimigo
            health--;

            // Se a vida chegou a zero, o inimigo morre e dá pontos
            if (health <= 0)
            {
                GameManager.Instance.AddScore(10); // Adiciona 10 pontos
                Destroy(gameObject); // Destrói o inimigo
            }
        }
    }
}