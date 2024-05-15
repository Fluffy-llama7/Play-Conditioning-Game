using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("End Menu");
    }
}
