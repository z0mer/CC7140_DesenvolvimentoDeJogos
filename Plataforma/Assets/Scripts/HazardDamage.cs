using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    public int damageAmount = 1; // Quanto de dano esse perigo dá

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checa se é o Player
        if (other.CompareTag("Player"))
        {
            // Tenta achar o script PlayerHealth nele
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // ---- MUDANÇA AQUI ----
                // Manda ele tomar dano E AVISA QUEM BATEU (o "transform" do cacto)
                playerHealth.TakeDamage(damageAmount, transform);
            }
        }
    }
}