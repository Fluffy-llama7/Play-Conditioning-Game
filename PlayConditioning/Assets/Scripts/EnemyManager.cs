using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject chargedEnemyPrefab;
    [SerializeField] private GameObject tankEnemyPrefab;
    [SerializeField] private GameObject bossEnemyPrefab;

    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;

    [SerializeField] private float minSpawnInterval = 1f; // Minimum spawn interval in seconds
    [SerializeField] private float maxSpawnInterval = 5f; // Maximum spawn interval in seconds

    // Serialized field for the number of enemies to spawn for each level
    [SerializeField] private int[] enemiesPerLevel = { 10, 15, 20, 25, 30, 35, 40 };

    private List<GameObject> enemyPrefabs;
    private GameManager gameManager;
    private int maxLevel = 7;

    private void Awake()
    {
        // Singleton pattern
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameManager = GameManager.instance;
    }

    private void Start()
    {
        enemyPrefabs = new List<GameObject> { basicEnemyPrefab, chargedEnemyPrefab, tankEnemyPrefab, bossEnemyPrefab };
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (gameManager.currentState != GameState.End && gameManager.currentState != GameState.Menu)
        {
            int currentLevel = (int)gameManager.currentState;

            if (currentLevel > maxLevel)
            {
                Debug.LogWarning("Current level exceeds max level defined.");
                yield break;
            }

            int enemiesToSpawn = CalculateEnemiesForLevel(currentLevel);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnRandomEnemy();
                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval)); // Random spawn interval
            }

            yield return null; // Wait for a frame before checking the game state again
        }
    }

    private int CalculateEnemiesForLevel(int level)
    {
        // Fetch the number of enemies for the specified level from the serialized array
        if (level >= 1 && level <= maxLevel)
        {
            return enemiesPerLevel[level - 1]; // Subtract 1 to account for zero-based indexing
        }
        else
        {
            return 0; // Return 0 for levels outside the specified range
        }
    }

    private void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        Vector2 randomPosition = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));
        Instantiate(enemyPrefabs[randomIndex], randomPosition, Quaternion.identity);
    }
}
