using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    public Text healthText;
    public Text speedText;
    public Text strengthText;
    public Text dodgeText;
    public Text aimText;

    void Start()
    {
        GameEventSystem.Instance.OnMouseHoverOnEnemy += UpdateStatistics;
    }

    void UpdateStatistics(Stats stats)
    {
        healthText.text = stats.health.ToString();
        speedText.text = stats.speed.ToString();
        strengthText.text = stats.strength.ToString();
        dodgeText.text = stats.dodge.ToString();
        aimText.text = stats.aim.ToString();
    }

    
}
