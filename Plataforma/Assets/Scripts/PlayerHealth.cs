using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    // --- NOVO: Variáveis do Knockback ---
    public float knockbackForce = 6f;    // Força pra trás
    public float knockbackUpForce = 4f;  // Força pra cima (o "hop")
    private Rigidbody2D rb;               // Referência à física

    // --- Do Pisca-Pisca (já tínhamos) ---
    public float invincibilityDuration = 2f;
    public float blinkDelay = 0.15f;
    private bool isInvincible = false;
    private SpriteRenderer playerSpriteRenderer;

    // --- NOVO: Referência pro UIManager ---
    private UIManager uiManager;

    void Start()
    {
        currentLives = maxLives;
        
        // Pega os componentes
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        
        // Acha o UIManager na cena
        uiManager = FindObjectOfType<UIManager>();

        // Atualiza a UI pra começar com 3 corações
        if(uiManager != null)
        {
            uiManager.UpdateHealthUI(currentLives);
        }
    }

    // ---- ATUALIZADO: Agora recebe o "Transform" do cacto ----
    public void TakeDamage(int damageAmount, Transform damageSource)
    {
        // Se o player estiver invencível (piscando), não faz NADA.
        if (isInvincible)
        {
            return;
        }

        currentLives -= damageAmount;
        Debug.Log("Ouch! Wall-E tomou dano! Vidas restantes: " + currentLives);

        // ---- NOVO: Manda o UIManager atualizar os corações ----
        if(uiManager != null)
        {
            uiManager.UpdateHealthUI(currentLives);
        }

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            // ---- MUDANÇA: Não dá mais Respawn! ----
            // Em vez disso, toma o tranco e pisca
            StartCoroutine(InvincibilityFrames());
            ApplyKnockback(damageSource);
        }
    }

    // ---- NOVO: A função do "Tranco" ----
    private void ApplyKnockback(Transform damageSource)
    {
        // Zera a velocidade atual pra dar um "tranco" limpo
        rb.linearVelocity = Vector2.zero;
        
        // Decide a direção: se o cacto tá à esquerda do player, o tranco é pra direita (1)
        // Se não, o tranco é pra esquerda (-1)
        float knockbackDirection = (transform.position.x > damageSource.position.x) ? 1 : -1;
        
        // Aplica a força!
        rb.AddForce(new Vector2(knockbackDirection * knockbackForce, knockbackUpForce), ForceMode2D.Impulse);
    }

    void Die()
    {
        Debug.Log("GAME OVER - Wall-E foi pro ferro-velho!");
        // (A gente vai mudar isso pra "Cena Final" depois)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- O Pisca-Pisca (Mesma coisa de antes) ---
    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        float invincibilityEndTime = Time.time + invincibilityDuration;

        while (Time.time < invincibilityEndTime)
        {
            playerSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkDelay);
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkDelay);
        }

        playerSpriteRenderer.enabled = true;
        isInvincible = false;
    }
}