// EnemyController.cs
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 4f;
    public int health = 1;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
            }
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            health--;
            if (health <= 0)
            {
                GameManager.Instance.AddScore(10);
                Destroy(gameObject);
            }
        }
    }
}