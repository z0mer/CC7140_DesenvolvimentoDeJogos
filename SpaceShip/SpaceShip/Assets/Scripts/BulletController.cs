// BulletController.cs
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // O GameManager adiciona os pontos, mas a lógica de dano
            // está no próprio inimigo, então só precisamos destruir.
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}