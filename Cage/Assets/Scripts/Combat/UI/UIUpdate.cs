using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemySkill;

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
            EnemyType et = stats.gameObject.GetComponent<EnemyProperties>().enemyType;

            if(et == EnemyType.CLOWN || et == EnemyType.CLOWN_CLONE)
                healthText.text = "?";
            else
                healthText.text = stats.health.ToString();

            speedText.text = stats.speed.ToString();

            int baseSpeed = Enemies.enemyStat[et]["speed"];

            if (stats.speed < baseSpeed)
                speedText.color = Color.red;
            else if (stats.speed == baseSpeed)
                speedText.color = Color.white;
            else
                speedText.color = Color.green;



            strengthText.text = stats.strength.ToString();
            int baseStrength = Enemies.enemyStat[et]["strength"];

            if (stats.strength < baseStrength)
                strengthText.color = Color.red;
            else if (stats.strength == baseStrength)
                strengthText.color = Color.white;
            else
                strengthText.color = Color.green;

            dodgeText.text = stats.dodge.ToString();

            int baseDodge = Enemies.enemyStat[et]["dodge"];

            if (stats.dodge < baseDodge)
                dodgeText.color = Color.red;
            else if (stats.dodge == baseDodge)
                dodgeText.color = Color.white;
            else
                dodgeText.color = Color.green;

            aimText.text = stats.aim.ToString();

            int baseAim = Enemies.enemyStat[et]["aim"];

            if (stats.aim < baseAim)
                aimText.color = Color.red;
            else if (stats.aim == baseAim)
                aimText.color = Color.white;
            else
                aimText.color = Color.green;

        }
        else
            UpdatePlayerStatistics(stats);
        
    }

    void UpdatePlayerStatistics(Stats stats)
    {
        playerHealthText.text = stats.health.ToString();
        playerSpeedText.text = stats.speed.ToString();

        if (stats.speed < stats.hero.speed)
            playerSpeedText.color = Color.red;
        else if(stats.speed == stats.hero.speed)
            playerSpeedText.color = Color.white;
        else
            playerSpeedText.color = Color.green;

        playerStrengthText.text = stats.strength.ToString();

        if (stats.strength < stats.hero.strength)
            playerStrengthText.color = Color.red;
        else if (stats.strength == stats.hero.strength)
            playerStrengthText.color = Color.white;
        else
            playerStrengthText.color = Color.green;

        playerDodgeText.text = stats.dodge.ToString();

        if (stats.dodge < stats.hero.dodge)
            playerDodgeText.color = Color.red;
        else if (stats.dodge == stats.hero.dodge)
            playerDodgeText.color = Color.white;
        else
            playerDodgeText.color = Color.green;

        playerAimText.text = stats.aim.ToString();

        if (stats.aim < stats.hero.accuracy)
            playerAimText.color = Color.red;
        else if (stats.aim == stats.hero.accuracy)
            playerAimText.color = Color.white;
        else
            playerAimText.color = Color.green;
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
