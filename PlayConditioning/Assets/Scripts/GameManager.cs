using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SceneController sceneController;
    public static GameManager instance { get; private set; }
    public GameState currentState { get; private set; }

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
    }

    private void Start()
    {
        this.UpdateState(GameState.Menu);
    }

    public void UpdateState(GameState newState)
    {
        // If the new state is the same as the current state, do nothing
        if (this.currentState == newState)
        {
            return;
        }

        this.currentState = newState;

        // Loads the scene based on the new state
        switch (newState)
        {
            case GameState.Menu:
                Debug.Log("Loading start scene");
                sceneController.LoadStartScene();
                break;
            case GameState.Tutorial:
                Debug.Log("Loading main scene");
                sceneController.LoadTutorial();
                break;
            case GameState.End:
                Debug.Log("Loading end scene");
                sceneController.LoadEndScene();
                break;
        }
    }
}
