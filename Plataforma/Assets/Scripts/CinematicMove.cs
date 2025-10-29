using UnityEngine;

public class CinematicMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform logoTarget; // Onde o Wall-E tem que parar (o logo)
    public GameObject dialogueBoxToTrigger; // A caixa de diálogo

    private bool reachedTarget = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Garante que ele tá olhando pra direita
        transform.localScale = new Vector3(1, 1, 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Se já chegou, não faz mais nada
        if (reachedTarget)
        {
            return;
        }

        // Checa se já passou do logo
        if (transform.position.x < logoTarget.position.x)
        {
            // Se não chegou, continua andando pra direita
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            // CHEGOU!
            reachedTarget = true;
            
            // Trava o Wall-E no lugar
            transform.position = new Vector3(logoTarget.position.x, transform.position.y, transform.position.z);
            
            // Acende o diálogo "Cheguei" [baseado na lógica de ativar/desativar do PDF 8]
            if (dialogueBoxToTrigger != null)
            {
                dialogueBoxToTrigger.SetActive(true);
            }
        }
    }
}