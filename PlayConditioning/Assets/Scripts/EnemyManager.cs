using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelEnemySpawnSettings
    {
        public int basicEnemies;
        public int chargedEnemies;
        public int tankEnemies;
        public int bossEnemies;
    }

    [SerializeField] private LevelEnemySpawnSettings[] enemiesSpawnSettings;
    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 5f;
    [SerializeField] private GameObject basicPrefab;
    [SerializeField] private GameObject chargedPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject bossPrefab;
    private Coroutine spawnCoroutine;
    private int enemiesKilled;
    private int totalEnemies;
    private int enemiesSpawned;
    public static EnemyManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Menu || newState == GameState.End)
        {
            StopSpawnCoroutine();
        }
        else
        {
            StartSpawnCoroutine();
        }
    }

    private void StartSpawnCoroutine()
    {
        StopSpawnCoroutine();
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void StopSpawnCoroutine()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        GameState state = GameManager.instance.currentState;
        int currentLevelIndex = (int)state - 1;

        if (currentLevelIndex >= 0 && currentLevelIndex < enemiesSpawnSettings.Length)
        {
            LevelEnemySpawnSettings currentSettings = enemiesSpawnSettings[currentLevelIndex];

            yield return new WaitForSeconds(2f);

            totalEnemies = CalculateTotalEnemies(currentSettings);
            enemiesSpawned = 0;

            while (enemiesSpawned < totalEnemies)
            {
                GameObject enemyPrefab = GetRandomEnemyPrefab(currentSettings);
                SpawnEnemy(enemyPrefab);
                enemiesSpawned++;
                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            }
        }
    }

    private int CalculateTotalEnemies(LevelEnemySpawnSettings settings)
    {
        return settings.basicEnemies +
               settings.chargedEnemies +
               settings.tankEnemies +
               settings.bossEnemies;
    }

    private GameObject GetRandomEnemyPrefab(LevelEnemySpawnSettings settings)
    {
        while (true)
        {
            int randomType = Random.Range(0, 4);
            switch (randomType)
            {
                case 0:
                    if (settings.basicEnemies > 0)
                    {
                        settings.basicEnemies--;
                        return basicPrefab;
                    }
                    break;
                case 1:
                    if (settings.chargedEnemies > 0)
                    {
                        settings.chargedEnemies--;
                        return chargedPrefab;
                    }
                    break;
                case 2:
                    if (settings.tankEnemies > 0)
                    {
                        settings.tankEnemies--;
                        return tankPrefab;
                    }
                    break;
                case 3:
                    if (settings.bossEnemies > 0)
                    {
                        settings.bossEnemies--;
                        return bossPrefab;
                    }
                    break;
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector2 randomPosition = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        Debug.Log("Enemies killed: " + enemiesKilled);

        if (enemiesKilled >= totalEnemies)
        {
            GameManager.instance.UpdateState(GameManager.instance.currentState + 1);
            enemiesKilled = 0;
        }
    }
}
