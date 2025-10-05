// BackgroundScroller.cs
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Renderer quadRenderer;
    private Vector2 textureOffset;

    void Start()
    {
        quadRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offsetX = (Time.time * scrollSpeed) % 1;
        textureOffset = new Vector2(offsetX, 0);
        quadRenderer.material.mainTextureOffset = textureOffset;
    }
}