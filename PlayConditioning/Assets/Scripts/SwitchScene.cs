using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: eventually use SceneManager.GetActiveScene.BuildIndex() + 1 to call on next level
        // instead of hardcoding every index.

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log(KeyCode.Alpha0 + " key was pressed");
            SceneManager.LoadScene(0);
        } 

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(KeyCode.Alpha1 + " key was pressed");
            SceneManager.LoadScene(1);
        } 
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(KeyCode.Alpha2 + " key was pressed");
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(KeyCode.Alpha3 + " key was pressed");
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(KeyCode.Alpha4 + " key was pressed");
            SceneManager.LoadScene(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log(KeyCode.Alpha5 + " key was pressed");
            SceneManager.LoadScene(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log(KeyCode.Alpha6 + " key was pressed");
            SceneManager.LoadScene(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log(KeyCode.Alpha7 + " key was pressed");
            SceneManager.LoadScene(7);
        }
    }
}
