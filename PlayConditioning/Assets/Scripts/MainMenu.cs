using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenuUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private TMP_Text versionText;

    // Start is called before the first frame update
    public void PlayGame()
    {
        GameManager.instance.UpdateState(GameState.Level1);
    }

    public void EnableOptions()
    {
        optionsMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
        Time.timeScale = 0f;
        UpdateVersionText();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Option1()
    {
        GameManager.instance.UpdateVersion(1);
        UpdateVersionText();
    }

    public void Option2()
    {
        GameManager.instance.UpdateVersion(2);
        UpdateVersionText();
    }

    public void Option3()
    {
        GameManager.instance.UpdateVersion(3);
        UpdateVersionText();
    }

    private void UpdateVersionText()
    {
        float version = GameManager.instance.GetVersion();
        versionText.text = "current game version: " + version;
    }
}
