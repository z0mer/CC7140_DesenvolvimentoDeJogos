using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Campo público pra gente "plugar" o Player
    public Transform playerTarget;

    // Usamos LateUpdate pra câmera. Ele roda *depois* que o Player
    // já se mexeu no 'Update', evitando que a câmera trema.
    void LateUpdate()
    {
        // Se a gente tiver um alvo (o Player)...
        if (playerTarget != null)
        {
            // A nova posição da câmera vai ser:
            // O X do Player, (OK)
            // O Y DA PRÓPRIA CÂMERA (essa é a mudança),
            // O Z original da câmera 
            transform.position = new Vector3(
                playerTarget.position.x, // Pega o X do jogador
                transform.position.y,    // Mas usa o Y da própria Câmera (fica parado na altura)
                transform.position.z     // Mantém o Z
            );
        }
    }
}