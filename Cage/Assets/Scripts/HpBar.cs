using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider slider;
    public Stats stats;

    private void Start()
    {
        slider.maxValue = stats.maxHealth;
        slider.value = stats.health;
    }
    public void changeHealth(int changeValue)
    {
        slider.value += changeValue;
    }


}
