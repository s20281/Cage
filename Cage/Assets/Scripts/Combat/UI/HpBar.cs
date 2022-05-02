using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider slider;
    public Slider shrinkSlider;
    public Stats stats;
    public float shrinkTimer;
    private const float SHRINK_TIME = 0.5f;
    float shrinkSpeed = 5f;


    private void Start()
    {
        slider.maxValue = stats.maxHealth;
        shrinkSlider.maxValue = stats.maxHealth;
        slider.value = stats.health;
        shrinkSlider.value = stats.health;
    }
    public void changeHealth(int changeValue)
    {
        shrinkTimer = SHRINK_TIME;
        slider.value += changeValue;
        shrinkSpeed = Math.Abs(changeValue) * 2;

        if (changeValue > 0)
            shrinkSlider.value += changeValue;
    }

    public void Update()
    {
        shrinkTimer -= Time.deltaTime;
        if(shrinkTimer < 0)
        {
           if(slider.value < shrinkSlider.value)
            {
                
                shrinkSlider.value -= shrinkSpeed * Time.deltaTime;
            }
        }
    }


}
