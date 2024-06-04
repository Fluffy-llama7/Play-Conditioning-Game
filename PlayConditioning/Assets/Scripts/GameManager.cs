using Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject version1UI;
    private GameObject version2UI;
    private GameObject version3UI;

    [SerializeField] private GameObject tutorialUIV1;
    [SerializeField] private GameObject tutorialUIV2;
    [SerializeField] private GameObject tutorialUIV3;

    private float gameVersion = 1;
    private List<IEnemy> enemies = new List<IEnemy>();
    private SceneController sceneController;

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

        sceneController = GetComponent<SceneController>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        this.UpdateState(GameState.Menu);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadUI();
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

    public void LoadHelp()
    {
        Time.timeScale = 0f;

        switch (gameVersion)
        {
            case 1:
                Instantiate(tutorialUIV1);
                break;
            case 2:
                Instantiate(tutorialUIV2);
                break;
            case 3:
                Instantiate(tutorialUIV3);
                break;
        }
    }

    public void EndHelp()
    {
        Time.timeScale = 1f;
    }

    public void LoadUI()
    {
        version1UI = GameObject.Find("uiV1");
        version2UI = GameObject.Find("uiV2");
        version3UI = GameObject.Find("uiV3");

        switch (gameVersion)
        {
            case 1:
                version2UI.SetActive(false);
                version3UI.SetActive(false);
                break;
            case 2:
                version1UI.SetActive(false);
                version3UI.SetActive(false);
                break;
            case 3:
                version1UI.SetActive(false);
                version2UI.SetActive(false);
                break;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            LoadHelp();
        }
    }
}
