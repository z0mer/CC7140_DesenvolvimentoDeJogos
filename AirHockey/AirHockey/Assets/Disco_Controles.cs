using UnityEngine;

public class PuckControl : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public AudioSource source;

    void GoPuck()
    {
        float rand = Random.Range(0, 2);
        if(rand < 1)
        {
            rb2d.AddForce(new Vector2(15, 20));
        }
        else
        {
            rb2d.AddForce(new Vector2(-15, -20));
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoPuck", 2);
        source = GetComponent<AudioSource>();
    }

    void ResetPuck()
    {
        rb2d.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetPuck();
        Invoke("GoPuck", 1);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        source.Play();
    }
}
