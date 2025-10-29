using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    // ---- ATUALIZADO AQUI ----
    private float startPosX;
    private float startPosY; // <-- NOVO: Posição Y inicial
    private GameObject cam;
    
    // Deixa 0.5 pra ele andar na "metade" da velocidade da câmera
    public float parallaxFactor; 

    void Start()
    {
        cam = Camera.main.gameObject;
        startPosX = transform.position.x;
        startPosY = transform.position.y; // <-- NOVO: Salva o Y inicial
    }

    void Update()
    {
        // ---- ATUALIZADO AQUI ----
        
        // Pega a "distância" que a câmera andou e multiplica pelo fator
        float dist_x = (cam.transform.position.x * parallaxFactor);
        float dist_y = (cam.transform.position.y * parallaxFactor); // <-- NOVO
        
        // Move o fundo um pouquinho em X E em Y
        transform.position = new Vector3(
            startPosX + dist_x, 
            startPosY + dist_y, // <-- ATUALIZADO AQUI
            transform.position.z
        );
    }
}