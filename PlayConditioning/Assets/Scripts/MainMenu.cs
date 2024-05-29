using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenuUI;


    // Start is called before the first frame update
    public void PlayGame()
    {
        GameManager.instance.UpdateState(GameState.Level1);
    }

    public void EnableOptions()
    {
        optionsMenuUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GoBack()
    {
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Option1()
    {
        GameManager.instance.UpdateVersion(1);
    }

    public void Option2()
    {
        GameManager.instance.UpdateVersion(2);
    }

    public void Option3()
    {
        GameManager.instance.UpdateVersion(3);
    }

    private void Update()
    {
        Debug.Log("Current Game Version: " + GameManager.instance.GetVersion());
    }
}
