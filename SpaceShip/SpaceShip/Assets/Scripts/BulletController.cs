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
        rb.linearVelocity = transform.up * speed;

        // Destroi o tiro depois de 3 segundos para não poluir a cena
        Destroy(gameObject, 3f);
    }

    // Se o tiro colidir com algo (marcado como Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Vamos checar se o objeto tem a tag "Enemy" (faremos isso no próximo passo)
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroi o inimigo
            Destroy(gameObject);      // Destroi o tiro
            // Aqui vamos adicionar a pontuação depois!
        }
    }
}