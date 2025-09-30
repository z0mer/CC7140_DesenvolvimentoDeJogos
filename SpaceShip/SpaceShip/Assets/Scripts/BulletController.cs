// BulletController.cs
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // O tiro sempre vai pra cima (no eixo Y local da nave)
        rb.linearVelocity = transform.right * speed;

        // Destroi o tiro depois de 3 segundos para não poluir a cena
        Destroy(gameObject, 3f);
    }

    // Se o tiro colidir com algo (marcado como Trigger)
    // BulletController.cs (trecho modificado)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // --- ADICIONA PONTOS CHAMANDO O GAMEMANAGER ---
            GameManager.Instance.AddScore(10); // Dá 10 pontos por inimigo
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}