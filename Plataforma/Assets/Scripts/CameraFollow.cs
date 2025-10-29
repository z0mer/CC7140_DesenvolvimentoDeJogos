using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Campo público pra gente "plugar" o Player
    public Transform playerTarget;

    // Usamos LateUpdate pra câmera (roda depois do Player)
    void LateUpdate()
    {
        // Se a gente tiver um alvo (o Player)...
        if (playerTarget != null)
        {
            // A nova posição da câmera vai ser:
            // O X do Player,
            // O Y do Player, (ANTES ESTAVA: transform.position.y)
            // O Z original da câmera
            transform.position = new Vector3(
                playerTarget.position.x, 
                playerTarget.position.y, // <-- MUDANÇA AQUI!
                transform.position.z 
            );
        }
    }
}