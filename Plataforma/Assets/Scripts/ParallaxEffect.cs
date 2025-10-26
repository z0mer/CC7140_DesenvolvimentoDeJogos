using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startPosX;
    private GameObject cam;
    public float parallaxFactor; // Bota 0.5 no Inspector pra testar

    void Start()
    {
        cam = Camera.main.gameObject;
        startPosX = transform.position.x;
    }

    void Update()
    {
        // Pega a distância que a câmera andou e multiplica pelo "fator"
        float dist = (cam.transform.position.x * parallaxFactor);
        // Move o fundo só um pouquinho
        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);
    }
}