using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Variáveis de movimento
    public float moveSpeed = 7f;
    public float jumpForce = 14f;
    public float climbSpeed = 5f;

    // Componentes e Checagens
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    // Variáveis de estado
    private float originalGravityScale;
    private bool isClimbing = false;
    
    // ---- NOVO AQUI ----
    private bool isFacingRight = true; // Começa olhando pra direita

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        if (Keyboard.current == null)
        {
            return; 
        }

        // --- 1. CHECAR O CHÃO ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // --- 2. PEGAR INPUT HORIZONTAL ---
        float moveInput = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveInput = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveInput = 1f;
        
        // ---- NOVO AQUI: LÓGICA DE "FLIPAR" O SPRITE ----
        if (moveInput > 0 && !isFacingRight)
        {
            Flip(); // Se tá andando pra direita mas olhando pra esquerda, vira.
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip(); // Se tá andando pra esquerda mas olhando pra direita, vira.
        }
        // Se moveInput == 0, ele fica olhando pro último lado que andou.

        // --- 3. LÓGICA DE ESTADO (ESCADA vs CHÃO) ---
        if (isClimbing)
        {
            // --- ESTÁ NA ESCADA ---
            rb.gravityScale = 0f; 

            float climbInput = 0f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                climbInput = 1f;
            else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                climbInput = -1f;
            
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, climbInput * climbSpeed);
        }
        else
        {
            // --- ESTÁ NO CHÃO ---
            rb.gravityScale = originalGravityScale;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        // --- 4. LÓGICA DE PULO (SEPARADA) ---
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
            }
            else if (isClimbing)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
                isClimbing = false; 
                rb.gravityScale = originalGravityScale;
            }
        }
    }
    
    // ---- NOVO AQUI: A FUNÇÃO DE "FLIPAR" ----
    private void Flip()
    {
        // Inverte a variável de controle
        isFacingRight = !isFacingRight;

        // Pega a escala local atual
        Vector3 theScale = transform.localScale;
        
        // Multiplica o X por -1 (inverte)
        theScale.x *= -1;
        
        // Aplica a nova escala de volta no transform
        transform.localScale = theScale;
    }

    // ---- MÉTODOS DE GATILHO (Trigger) ----
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = originalGravityScale; 
        }
    }
}