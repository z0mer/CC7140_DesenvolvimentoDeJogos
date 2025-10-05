// PlayerController.cs
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Invincibility")]
    public float invincibilityDuration = 3f; // 3 segundos de invencibilidade
    private bool isInvincible = false;

    // Componentes e Limites
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    private Vector2 shipBounds;

    void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateScreenBounds();
        StartCoroutine(BecomeInvincible());
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void LateUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }

    public void TakeDamage()
    {
        if (isInvincible)
        {
            return;
        }
        GameManager.Instance.PlayerTakesDamage();
        Destroy(gameObject);
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        float endTime = Time.time + invincibilityDuration;
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void CalculateScreenBounds()
    {
        shipBounds = spriteRenderer.bounds.extents;
        Vector2 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector2 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        minX = screenBottomLeft.x + shipBounds.x;
        maxX = screenTopRight.x - shipBounds.x;
        minY = screenBottomLeft.y + shipBounds.y;
        maxY = screenTopRight.y - shipBounds.y;
    }

    // --- Input System Callbacks ---
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
}