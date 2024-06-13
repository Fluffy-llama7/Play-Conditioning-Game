using Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float gameVersion = 1;
    private List<IEnemy> enemies = new List<IEnemy>();
    private SceneController sceneController;
    private int enemiesKilled = 0; // Track the number of enemies killed
    private int killThreshold = 10; // Initial kill threshold for Level 1

    public GameState currentState { get; private set; }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sceneController = GetComponent<SceneController>();
    }

    private void Start()
    {
        this.UpdateState(GameState.Menu);
    }

    public void UpdateState(GameState newState)
    {
        if (this.currentState == newState)
        {
            return;
        }

        this.currentState = newState;

        switch (newState)
        {
            case GameState.Menu:
                this.sceneController.LoadStartScene();
                break;
            case GameState.Level1:
                PrepareNextLevel(10);
                break;
            case GameState.Level2:
                PrepareNextLevel(15);
                break;
            case GameState.Level3:
                PrepareNextLevel(20);
                break;
            case GameState.Level4:
                PrepareNextLevel(25);
                break;
            case GameState.Level5:
                PrepareNextLevel(30);
                break;
            case GameState.Level6:
                PrepareNextLevel(35);
                break;
            case GameState.Level7:
                PrepareNextLevel(40);
                break;
            case GameState.End:
                this.sceneController.LoadEndScene();

                if (UIManager.instance)
                {
                    Destroy(UIManager.instance.gameObject);
                    UIManager.instance = null;
                }

                if (EnemyManager.instance)
                {
                    Destroy(EnemyManager.instance.gameObject);
                    EnemyManager.instance = null;
                }

                enemies.Clear();
                break;
        }
    }

    public void AddEnemy(IEnemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(IEnemy enemy)
    {
        enemies.Remove(enemy);
        enemiesKilled++;

        // Debug statement to track enemy removal
        Debug.Log($"Enemy killed: {enemiesKilled}/{killThreshold}");

        if (enemiesKilled >= killThreshold && currentState != GameState.End)
        {
            // Debug statement to confirm level progression
            Debug.Log("Kill threshold reached. Loading next level...");
            UpdateState(currentState + 1); // Move to the next level
        }
    }

    private void PrepareNextLevel(int threshold)
    {
        ResetKillCounter(threshold);
        this.sceneController.LoadNextLevel();
    }

    private void ResetKillCounter(int threshold)
    {
        enemiesKilled = 0;
        killThreshold = threshold;

        // Debug statement to track reset of kill counter
        Debug.Log($"Kill counter reset. New kill threshold: {killThreshold}");
    }

    public void UpdateVersion(int version)
    {
        gameVersion = version;
    }

    public float GetVersion()
    {
        return gameVersion;
    }
}
