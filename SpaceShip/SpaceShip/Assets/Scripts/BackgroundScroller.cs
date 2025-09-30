// BackgroundScroller.cs
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // Velocidade que o fundo vai se mover. Use valores pequenos!
    public float scrollSpeed = 0.1f;

    // A referência ao Renderer do nosso Quad
    private Renderer quadRenderer;
    // O valor do "deslocamento" da textura
    private Vector2 textureOffset;

    void Start()
    {
        // Pega o componente Renderer do objeto (o Quad)
        quadRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // A mágica acontece aqui:
        // A cada frame, a gente calcula um novo deslocamento no eixo X.
        // Usamos Time.time para um movimento contínuo e suave.
        // O "% 1" (módulo de 1) garante que o valor nunca passe de 1, o que não é estritamente
        // necessário com o Wrap Mode = Repeat, mas é uma boa prática.
        float offsetX = (Time.time * scrollSpeed) % 1;

        // Criamos o vetor de deslocamento (só movemos no X)
        textureOffset = new Vector2(offsetX, 0);

        // Aplicamos esse deslocamento ao material do nosso Quad
        quadRenderer.material.mainTextureOffset = textureOffset;
    }
}