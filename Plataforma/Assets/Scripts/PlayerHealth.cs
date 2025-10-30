using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Essencial!

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    // --- Variáveis do Knockback ---
    public float knockbackForce = 6f;
    public float knockbackUpForce = 4f;
    private Rigidbody2D rb;

    // --- Do Pisca-Pisca ---
    public float invincibilityDuration = 2f;
    public float blinkDelay = 0.15f;
    private bool isInvincible = false;
    private SpriteRenderer playerSpriteRenderer;

    // --- Referência pro UIManager ---
    private UIManager uiManager;

    void Start()
    {
        currentLives = maxLives;
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        uiManager = FindObjectOfType<UIManager>();

        if(uiManager != null)
        {
            uiManager.UpdateHealthUI(currentLives);
        }
    }

    public void TakeDamage(int damageAmount, Transform damageSource)
    {
        if (isInvincible)
        {
            return;
        }

        currentLives -= damageAmount;

        if(uiManager != null)
        {
            uiManager.UpdateHealthUI(currentLives);
        }

        if (currentLives <= 0)
        {
            // --- AQUI É A MUDANÇA ---
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
            ApplyKnockback(damageSource);
        }
    }

    private void ApplyKnockback(Transform damageSource)
    {
        rb.linearVelocity = Vector2.zero;
        float knockbackDirection = (transform.position.x > damageSource.position.x) ? 1 : -1;
        rb.AddForce(new Vector2(knockbackDirection * knockbackForce, knockbackUpForce), ForceMode2D.Impulse);
    }

    // ---- A FUNCAO DIE() FOI ATUALIZADA ----
    void Die()
    {
        Debug.Log("GAME OVER - Wall-E foi pro ferro-velho!");
        
        // Em vez de recarregar a cena, ele agora CARREGA A CENA DE DERROTA!
        SceneManager.LoadScene("LoseScene");
    }

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