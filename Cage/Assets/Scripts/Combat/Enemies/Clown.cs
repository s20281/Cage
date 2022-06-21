using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySkill;

public class Clown : MonoBehaviour
{
    // STATS

    public readonly int maxHealth = 20;
    public readonly int speed = 10;
    public readonly int strength = 5;
    public readonly int dodge = 3;
    public readonly int aim = 6;

    // LOGIC

    bool isReal;


    public Action AI()
    {
        isReal = gameObject.GetComponent<EnemyProperties>().enemyType == EnemyType.CLOWN;

        if (isReal && Turn.control.aliveEnemiesCount == 1)
            return summonClones;


        return hammer;
    }

    // SKILLS

    private void hammer()
    {
        if (!EnemySkill.hit())
            return;

        EnemySkill.dealDmg(5);
    }

    private void summonClones()
    {
        LoadCharacters.control.summon(EnemyType.CLOWN_CLONE, 3);
    }

    private void shuffle()
    {

    }

}
