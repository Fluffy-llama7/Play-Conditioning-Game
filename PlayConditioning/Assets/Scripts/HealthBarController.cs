using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    public Slider slide;
 
    public void SetHealth(float health)
    {
        slide.value = health;
    }

    public void SetMaxHealth(float maxHealth)
    {
        slide.maxValue = maxHealth;
        slide.value = maxHealth;
    }
}
