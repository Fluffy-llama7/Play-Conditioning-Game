using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UIScript : MonoBehaviour
{
    private static UIScript singleton;
    private TextMeshProUGUI enemiesKilledText, healthText, mechanicText;
    private int enemiesKilled;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        Debug.Log(this.transform.childCount);
        enemiesKilledText = (TextMeshProUGUI)GameObject.Find("UI Enemy Text").GetComponent("TextMeshProUGUI");
        healthText = (TextMeshProUGUI)GameObject.Find("UI Health Text").GetComponent("TextMeshProUGUI");
        mechanicText = (TextMeshProUGUI)GameObject.Find("UI Mechanic Text").GetComponent("TextMeshProUGUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static UIScript getSingleton(){
        return singleton;
    }
    public void incrementEnemyKilledText(){
        enemiesKilled++;
        enemiesKilledText.text = "x " + enemiesKilled.ToString();
    }
    public void resetEnemyKilledText(){
        enemiesKilled = 0;
    }
    public void setHealthText(float health){
        this.health = health;
        // Health should be between 0 and 100 (?)
        Math.Clamp(health, 0f, 100f);
        healthText.text = Math.Floor(health).ToString();
    }
    public void setMechanicText(string text){
        mechanicText.text = text;
    }
}
