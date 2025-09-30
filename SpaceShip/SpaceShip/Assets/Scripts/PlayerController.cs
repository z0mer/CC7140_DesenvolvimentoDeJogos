// PlayerController.cs (versão com limites de tela)
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // --- NOVAS VARIÁVEIS PARA OS LIMITES ---
    private float minX, maxX, minY, maxY;
    private Camera mainCamera;
    private Vector2 shipBounds;

    void Awake()
    {
        mainCamera = Camera.main; // Pega a câmera principal da cena
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateScreenBounds(); // Chama nossa nova função pra calcular os limites
    }

    // A física continua rodando normalmente no FixedUpdate
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    // --- NOVO MÉTODO: LATEUPDATE ---
    // LateUpdate roda depois de todos os outros Updates (incluindo o da física).
    // É o lugar perfeito para fazer ajustes finais de posição.
    void LateUpdate()
    {
        // Pega a posição atual da nave
        Vector3 clampedPosition = transform.position;

        // Usa a função Mathf.Clamp para "prender" os valores de X e Y dentro dos nossos limites
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        // Aplica a nova posição (já corrigida) de volta na nave
        transform.position = clampedPosition;
    }

    // --- NOSSAS FUNÇÕES DE INPUT (CONTINUAM IGUAIS) ---
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    
    // --- NOVA FUNÇÃO PARA CALCULAR OS LIMITES DA TELA ---
    void CalculateScreenBounds()
    {
        // Descobre a "metade do tamanho" do sprite da nave.
        // Isso é importante para a borda da nave bater na borda da tela, e não o centro dela.
        shipBounds = GetComponent<SpriteRenderer>().bounds.extents;

        // Pede pra câmera as coordenadas das bordas em unidades da Unity
        Vector2 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector2 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Calcula os limites exatos para a posição da NAVE
        minX = screenBottomLeft.x + shipBounds.x;
        maxX = screenTopRight.x - shipBounds.x;
        minY = screenBottomLeft.y + shipBounds.y;
        maxY = screenTopRight.y - shipBounds.y;
    }
}