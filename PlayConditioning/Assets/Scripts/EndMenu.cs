using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public void MainMenu()
    {
        GameManager.instance.UpdateState(GameState.Menu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
