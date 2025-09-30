// EnemySpawner.cs (versão com Gizmo visual)
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRate = 1.5f;
    public float spawnAreaHeight = 9f;

    void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];

            float randomY = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0);

            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // --- FUNÇÃO NOVA ADICIONADA ---
    // Isso desenha uma linha na tela de Scene View pra gente ver a área de spawn
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green; // A linha vai ser verde
        // Ponto inicial da linha (topo da área de spawn)
        Vector3 topPoint = new Vector3(transform.position.x, transform.position.y + spawnAreaHeight / 2, 0);
        // Ponto final da linha (base da área de spawn)
        Vector3 bottomPoint = new Vector3(transform.position.x, transform.position.y - spawnAreaHeight / 2, 0);
        // Desenha a linha
        Gizmos.DrawLine(topPoint, bottomPoint);
    }
}