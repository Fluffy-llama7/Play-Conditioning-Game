using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
    private float gameVersion = 1;
    private List <IEnemy> enemies = new List<IEnemy>();
    public static GameManager instance;
    private SceneController sceneController;
    public GameState currentState { get; private set;}
    public static event Action<float, float> OnHealthChange;

    void Awake()
    {
        if (!GameManager.instance)
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
                break;
        }
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        OnHealthChange?.Invoke(currentHealth, maxHealth);
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
        switch (version)
        {
            case 1:
                gameVersion = 1;
                break;
            case 2:
                gameVersion = 2;
                break;
            case 3:
                gameVersion = 3;
                break;
        }
    }

    public float GetVersion()
    {
        return gameVersion;
    }
}
