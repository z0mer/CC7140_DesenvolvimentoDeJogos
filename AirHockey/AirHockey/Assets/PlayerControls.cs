using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public float X = 3.44f;
    public float Y = 6.33f;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var pos = transform.position;

        pos.x = mousePos.x;
        pos.y = mousePos.y;

        if (pos.x > X) pos.x = X;
        else if (pos.x < -X) pos.x = -X;

        if (pos.y > 0) pos.y = 0;
        else if (pos.y < -Y) pos.y = -Y;

        transform.position = pos;
    }
}
