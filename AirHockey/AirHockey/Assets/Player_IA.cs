using UnityEngine;

public class Player_IA : MonoBehaviour
{
    public float X = 3.44f;
    public float Y = 2.25f;

    public float Speed = 3.0f;

    private Vector2 Direcao;

    void Start()
    {
        DirecaoAleatoria();
    }

    void Update()
    {
        Vector3 newPosition = transform.position + (Vector3)Direcao * Speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -X, X);
        newPosition.y = Mathf.Clamp(newPosition.y, 0f, 6.33f);

        transform.position = newPosition;

        AjustarDirecao();
    }
    void DirecaoAleatoria()
    {
        Direcao = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void AjustarDirecao()
    {
        var pos = transform.position;

        if (pos.x >= X || pos.x <= - X)
        {
            Direcao.x *= -1;
            Direcao.y = Random.Range(0f, 1f);
        }
        if (pos.y >= 6.33f || pos.y <= 0f)
        {
            Direcao.y *= -1;
            Direcao.x = Random.Range(-1f, 1f);
        }
        Direcao.Normalize();
    }
}
