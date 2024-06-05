using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject uiV1;
    public GameObject uiV2;
    public GameObject uiV3;

    public GameObject tutorialUIV1;
    public GameObject tutorialUIV2;
    public GameObject tutorialUIV3;

    private bool isTutorialActive;

    void Awake()
    {
        if (!UIManager.instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadTutorialUI();
    }

    void LoadVersionUI()
    {
        switch (GameManager.instance.GetVersion())
        {
            case 1:
                uiV1.SetActive(true);
                break;
            case 2:
                uiV2.SetActive(true);
                break;
            case 3:
                uiV3.SetActive(true);
                break;
        }
    }

    public void LoadTutorialUI()
    {
        Time.timeScale = 0f;
        isTutorialActive = true;

        switch (GameManager.instance.GetVersion())
        {
            case 1:
                tutorialUIV1.SetActive(true);
                break;
            case 2:
                tutorialUIV2.SetActive(true);
                break;
            case 3:
                tutorialUIV3.SetActive(true);
                break;
        }
    }

    public void CloseTutorialUI()
    {
        Time.timeScale = 1f;
        tutorialUIV1.SetActive(false);
        tutorialUIV2.SetActive(false);
        tutorialUIV3.SetActive(false);
        isTutorialActive = false;
    }

    private void Update()
    {
        LoadVersionUI();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isTutorialActive)
            {
                CloseTutorialUI();
            }
            else
            {
                LoadTutorialUI();
            }
        }
    }
}
