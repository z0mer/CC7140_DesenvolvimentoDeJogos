// PlayerController.cs (versão atualizada)
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // --- NOVAS VARIÁVEIS PARA O TIRO ---
    public GameObject bulletPrefab; // O prefab do nosso tiro
    public Transform firePoint;     // O local de onde o tiro sai
    public float fireRate = 0.5f;   // Tiros por segundo
    private float nextFire = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // --- LÓGICA DO TIRO ---
        // Se o botão "Fire1" (mouse esquerdo ou Ctrl) for pressionado e o tempo de espera passou
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1/fireRate;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }

    // --- NOVA FUNÇÃO DE ATIRAR ---
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}