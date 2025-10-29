using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Você disse 10 pontos
    public int scoreValue = 10; 

    public AudioClip collectSound;

    // Roda quando alguém ENCOSTA no gatilho
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checa se quem encostou é o Player (o Wall-E)
        if (other.CompareTag("Player"))
        {
            // Se for, acha o "chefe" GameManager e manda ele adicionar os pontos
            GameManager.instance.AddScore(scoreValue);
            
            if (collectSound != null) { AudioSource.PlayClipAtPoint(collectSound, transform.position); }

            // Destrói a plantinha (o objeto)
            Destroy(gameObject);
        }
    }
}