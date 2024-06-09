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

    [SerializeField] private int initialEnemies = 10; // Number of enemies spawning initially
    [SerializeField] private int additionalEnemiesPerLevel = 5; // Number of additional enemies to spawn after each level

    private List<GameObject> enemyPrefabs;
    private int currentLevel = 1;
    private int enemiesPerLevel;
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
    }

    private void Start()
    {
        enemyPrefabs = new List<GameObject> { basicEnemyPrefab, chargedEnemyPrefab, tankEnemyPrefab, bossEnemyPrefab };
        enemiesPerLevel = initialEnemies;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentLevel <= maxLevel)
        {
            // Check if the game is in a level state
            if (GameManager.instance.currentState != GameState.Menu && GameManager.instance.currentState != GameState.End)
            {
                for (int i = 0; i < enemiesPerLevel; i++)
                {
                    SpawnRandomEnemy();
                    yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval)); // Random spawn interval
                }
                currentLevel++;
                enemiesPerLevel += additionalEnemiesPerLevel;
            }
            else
            {
                // If not in a level state, wait for a short time before checking again
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void SpawnRandomEnemy()
    {
        int randomIndex;

        // Exclude boss enemies for the first 3 levels
        if (currentLevel < 4)
        {
            randomIndex = Random.Range(0, enemyPrefabs.Count - 1); // Exclude the last element (boss)
        }
        else
        {
            randomIndex = Random.Range(0, enemyPrefabs.Count); // Include all enemies
        }

        Vector2 randomPosition = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));
        Instantiate(enemyPrefabs[randomIndex], randomPosition, Quaternion.identity);
    }
}
