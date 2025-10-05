// EnemySpawner.cs
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRate = 1.5f;
    
    private float spawnAreaHeight;

    void Start()
    {
        spawnAreaHeight = Camera.main.orthographicSize * 2;
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
}