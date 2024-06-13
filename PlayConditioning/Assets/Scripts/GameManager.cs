using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float gameVersion = 1;
    private SceneController sceneController;
    public GameState currentState { get; private set; }
    public static event Action<GameState> OnGameStateChanged;

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
        if (this.currentState == newState) return;

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

        OnGameStateChanged?.Invoke(newState);
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
