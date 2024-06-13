using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject controlsV1;
    [SerializeField] private GameObject controlsV2;
    [SerializeField] private GameObject controlsV3;
    [SerializeField] private GameObject tutorialV1;
    [SerializeField] private GameObject tutorialV2;
    [SerializeField] private GameObject tutorialV3;
    private bool isTutorialActive;
    public static UIManager instance;

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

        LoadTutorialUI();
    }

    void LoadVersionUI()
    {
        switch (GameManager.instance.GetVersion())
        {
            case 1:
                controlsV1.SetActive(true);
                break;
            case 2:
                controlsV2.SetActive(true);
                break;
            case 3:
                controlsV3.SetActive(true);
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
                tutorialV1.SetActive(true);
                break;
            case 2:
                tutorialV2.SetActive(true);
                break;
            case 3:
                tutorialV3.SetActive(true);
                break;
        }
    }

    public void CloseTutorialUI()
    {
        Time.timeScale = 1f;
        tutorialV1.SetActive(false);
        tutorialV2.SetActive(false);
        tutorialV3.SetActive(false);
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
