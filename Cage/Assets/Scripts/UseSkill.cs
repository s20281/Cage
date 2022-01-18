using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour
{
    public int activeSkill;

    public Stats playerStats;
    Stats enemyStats;
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
            switch(activeSkill)
            {
                case 1:
                    simpleAtack(playerStats, enemyStats);
                    break;
                default:
                    break;
            }
        }
    }

            public void setTarget(Stats stats)
    {
        hasTarget = true;
        enemyStats = stats;
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

        if(roll + modificator < 0)
        {
            // Miss
            return;
        }

        int damage = 3 + playerStats.strength;
        enemyStats.onHealthChange(-damage);
    }

}
