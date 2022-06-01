using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySkill;

public class Witch : MonoBehaviour
{
    // STATS

    public readonly int maxHealth = 10;
    public readonly int speed = 8;
    public readonly int strength = 2;
    public readonly int dodge = 3;
    public readonly int aim = 7;

    // LOGIC

    bool usedSpecialPower = false;
    bool healNextTurn = false;
    bool mustHeal = false;

    public Action AI()
    {
        if(!usedSpecialPower && Turn.control.turnNumber > 1)
        {
            usedSpecialPower = true;
            return witchSummon;
        }

        if (gameObject.GetComponent<Stats>().health < 6)
            mustHeal = true;

        if (mustHeal)
        {
            mustHeal = false;
            return witchHeal;
        }

        bool dontHeal = true;

        List<Stats> enemies = Turn.control.getAliveEnemies();
        foreach (Stats enemy in enemies)
        {
            if (enemy.gameObject.GetComponent<EnemyProperties>().enemyType == EnemyType.TORNADO && enemy.health == 1)
            {
                healNextTurn = true;
            }
            else if (enemy.health < enemy.maxHealth)
                dontHeal = false;
        }

        if(healNextTurn)
        {
            healNextTurn = false;
            mustHeal = true;
            return witchBasic;
        }

        int rand = UnityEngine.Random.Range(0, 4);

        if (dontHeal && usedSpecialPower)
            return witchBasic;

        if (dontHeal)
            rand++;

        switch (rand)
        {
            case 0:
                return witchHeal;

            case 1:
                if (!usedSpecialPower && Turn.control.aliveEnemiesCount < 4)
                {
                    usedSpecialPower = true;
                    return witchSummon;
                }
                else
                    return AI();

            
            default:
                return witchBasic;
        }
    }

    // SKILLS

    private void witchBasic()
    {
        if (!EnemySkill.hit())
            return;
        attackAnimation();
        StartCoroutine(animationDelay());
    }

    IEnumerator animationDelay()
    {
        yield return new WaitForSeconds(0.5f);
        EnemySkill.dealDmg(4);
    }


    private void witchHeal()
    {
        List<Stats> aliveEnemies = Turn.control.getAliveEnemies();

        foreach (Stats s in aliveEnemies)
        {
            s.healthChange(3);
        }
    }

    private void witchSummon()
    {
        LoadCharacters.control.summon(EnemyType.TORNADO, 1);
    }

    //ANIMATIONS

    public void deathAnimation()
    {
        gameObject.GetComponent<Animator>().SetTrigger("death");
    }

    public void hitAnimation()
    {
        gameObject.GetComponent<Animator>().SetTrigger("hit");
    }

    public void attackAnimation()
    {
        gameObject.GetComponent<Animator>().SetTrigger("attack");
    }
}
