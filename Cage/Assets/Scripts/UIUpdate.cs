using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    public GameObject enemyStatsPanel;
    public Text healthText;
    public Text speedText;
    public Text strengthText;
    public Text dodgeText;
    public Text aimText;

    public GameObject playerStatsPanel;
    public Text playerHealthText;
    public Text playerSpeedText;
    public Text playerStrengthText;
    public Text playerDodgeText;
    public Text playerAimText;

    public Image select1;
    public Image select2;
    public Image select3;
    public Image select4;

    void Start()
    {
        GameEventSystem.Instance.OnMouseOverEnemy += UpdateStatistics;
        GameEventSystem.Instance.OnPositiveSkillUse += UpdatePlayerStatistics;
        GameEventSystem.Instance.OnSkillUse += deselectSkills;
        GameEventSystem.Instance.OnEnemyStatsActive += activateEnemyStats;
        GameEventSystem.Instance.OnPlayerStatsActive += activatePlayerStats;
    }

    void UpdateStatistics(Stats stats)
    {
        if (stats.gameObject.CompareTag("Enemy"))
        {
            healthText.text = stats.health.ToString();
            speedText.text = stats.speed.ToString();
            strengthText.text = stats.strength.ToString();
            dodgeText.text = stats.dodge.ToString();
            aimText.text = stats.aim.ToString();
        }
        else
            UpdatePlayerStatistics(stats);
        
    }

    void UpdatePlayerStatistics(Stats stats)
    {
        playerHealthText.text = stats.health.ToString();
        playerSpeedText.text = stats.speed.ToString();
        playerStrengthText.text = stats.strength.ToString();
        playerDodgeText.text = stats.dodge.ToString();
        playerAimText.text = stats.aim.ToString();
    }

    void deselectSkills()
    {
        select1.gameObject.SetActive(false);
        select2.gameObject.SetActive(false);
        select3.gameObject.SetActive(false);
        select4.gameObject.SetActive(false);
    }

    void activatePlayerStats(bool isActive)
    {
        playerStatsPanel.SetActive(isActive);
    }

    void activateEnemyStats(bool isActive)
    {
        enemyStatsPanel.SetActive(isActive);
    }

}
