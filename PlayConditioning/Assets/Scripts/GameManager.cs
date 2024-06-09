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
            case GameState.Level2:
            case GameState.Level3:
            case GameState.Level4:
            case GameState.Level5:
            case GameState.Level6:
            case GameState.Level7:
                this.sceneController.LoadNextLevel();
                break;
            case GameState.End:
                this.sceneController.LoadEndScene();

                if (UIManager.instance)
                {
                    Destroy(UIManager.instance.gameObject);
                    UIManager.instance = null;
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

        if (enemies.Count == 0 && currentState != GameState.End)
        {
            sceneController.LoadNextLevel();
        }
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
