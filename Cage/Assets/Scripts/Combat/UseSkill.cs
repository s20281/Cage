using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSkill : MonoBehaviour
{
    public int activeSkill;

    public Stats playerStats;
    public Stats enemyStats;
    public bool hasTarget;

    void Start()
    {
        activeSkill = 0;
        GameEventSystem.Instance.OnMouseOverEnemy += setTarget;
        GameEventSystem.Instance.OnMouseExitEnemy += unsetTarget;
        hasTarget = false;
    }

    void Update()
    {
        if (hasTarget && Input.GetMouseButtonDown(0))
        {
            switch (activeSkill)
            {
                case 1:
                    simpleAtack(playerStats, enemyStats);
                    break;
                case 2:
                    protection(playerStats, enemyStats);
                    break;
                case 3:
                    heal(playerStats, enemyStats);
                    break;
                default:
                    break;
            }
            activeSkill = 0;
            GameEventSystem.Instance.SetSkillUse();
            Destroy(playerStats.queueIcon);
        }
    }

    public void setTarget(Stats stats)
    {
        if(!stats.isDead)
        {
            hasTarget = true;
            enemyStats = stats;
        }
    }

    public void unsetTarget(Stats stats)
    {
        hasTarget = false;
    }

    public void changeActiveSkillTo1()
    {
        activeSkill = 1;
    }
    public void changeActiveSkillTo2()
    {
        activeSkill = 2;
    }
    public void changeActiveSkillTo3()
    {
        activeSkill = 3;
    }
    public void changeActiveSkillTo4()
    {
        activeSkill = 4;
    }

    private void simpleAtack(Stats playerStats, Stats enemyStats)
    {
        float modificator = playerStats.aim - enemyStats.dodge;
        float roll = Random.Range(-5, 5);
        GameObject enemy = enemyStats.gameObject;
        GameObject player = playerStats.gameObject;

        if (roll + modificator < 0)
        {
            // Miss
            Debug.Log("Miss");
            
            enemy.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("DODGE", Color.green);
            player.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("MISS", Color.yellow);
            return;
        }

        int damage = 3 + playerStats.strength;
        enemy.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect(damage.ToString(), Color.red);
        enemyStats.onHealthChange(-damage);
        Debug.Log("Player attacks for " + damage);
    }

    private void heal(Stats playerStats, Stats enemyStats)
    {
        int roll = Random.Range(3, 5);
        enemyStats.onHealthChange(roll);
        GameEventSystem.Instance.SetPositiveSkillUse(enemyStats);
        Debug.Log("Player heals for " + roll);
    }

    private void protection(Stats playerStats, Stats enemyStats)
    {
        enemyStats.dodge += 3;
        GameEventSystem.Instance.SetPositiveSkillUse(enemyStats);

        Debug.Log("Player heals increases he's dodge for " + 3);
    }

}
