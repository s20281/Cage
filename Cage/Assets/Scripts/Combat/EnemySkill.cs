using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemySkill
{
    public enum EnemyType { SHADOW, ROCK, SPIDER, ZOMBIE, CLOWN, WITCH, RAT, TORNADO, NONE }


    public static Dictionary<EnemyType, List<Action>> skillMaping;

    static Stats caster;
    static Stats target;

    public static void Start()
    {
        skillMaping = new Dictionary<EnemyType, List<Action>>();

        // SHADOW

        // ROCK
        List<Action> rockSkills = new List<Action>();
        rockSkills.Add(rockStun);
        skillMaping.Add(EnemyType.ROCK, rockSkills);

        // SPIDER
        List<Action> spiderSkills = new List<Action>();
        spiderSkills.Add(spiderWeb);
        spiderSkills.Add(spiderVenom);
        skillMaping.Add(EnemyType.SPIDER, spiderSkills);

        //ZOMBIE
        List<Action> zombieSkills = new List<Action>();
        zombieSkills.Add(zombieHeal);
        skillMaping.Add(EnemyType.ZOMBIE, zombieSkills);



    }

    public static void UseSkill(Stats c, Stats t)
    {
        caster = c;
        target = t;

        if (target.isProtected && !target.Protector.isDead)
        {
            Debug.Log(target.Protector.name + " defends " + target.name);
            target = target.Protector;  
        }
            

        EnemyType enemyType = caster.gameObject.GetComponent<EnemyProperties>().enemyType;
        chooseSKill(enemyType)();
    }

    private static Action chooseSKill(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.SHADOW:
                return shadowBasic;

            case EnemyType.ROCK:
                int rand = UnityEngine.Random.Range(0, 5);
                switch (rand)
                {
                    case 0:
                        return rockShield;
                    case 1:
                    case 2:
                        return rockStun;

                    default:
                        return rockBasic;
                }

            case EnemyType.SPIDER:
                rand = UnityEngine.Random.Range(0, 1);
                return skillMaping[enemyType][rand];

            case EnemyType.ZOMBIE:
                if (caster.health < caster.maxHealth && caster.effectsList.Count == 0)  // TODO Sprawdzenie czy ten efekt to heal
                    return zombieHeal;
                else
                    return zombieBasic;

            case EnemyType.CLOWN:
                return clownBasic;

            case EnemyType.WITCH:
                rand = UnityEngine.Random.Range(0, 4);
                switch (rand)
                {
                    case 0:
                        if (!caster.usedSpecialPower && Turn.control.aliveEnemiesCount < 4)
                        {
                            caster.usedSpecialPower = true;
                            return witchSummon;
                        }
                        else
                            return chooseSKill(enemyType);
                       
                    case 1:
                        return witchHeal;
                    default:
                        return witchBasic;
                }
            case EnemyType.RAT:
                if (Turn.control.aliveEnemiesCount < 4)
                    return ratSummonRats;
                else
                    return ratBasic;

            case EnemyType.TORNADO:
                if (caster.health == 2)
                    return tornadoBuff;
                else
                    return tornadoBasic;

            default:
                return skipTurn;
        }
    }


    private static bool hit()
    {
        int aim = caster.aim;
        int dodge = target.dodge;
        int diff = aim - dodge;

        int score = UnityEngine.Random.Range(-10, 30) + diff;

        if (score < 0)
        {
            Debug.Log(caster.name + " missed");
            caster.gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("MISS", Color.yellow);
            target.gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("DODGE", Color.green);
            return false;
        }
        return true;
    }

    private static void dealDmg(int damage)
    {
        target.healthChange(-damage);
        target.gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect(damage.ToString(), Color.red);
    }

    private static void shadowBasic()
    {
        if (!hit()) return;
        dealDmg(3);
    }

    private static void spiderWeb()
    {
        if (!hit()) return;
        dealDmg(2);
        Buff web = new Buff(-3, 0, -3, 0, 2);
        target.addBuff(web);
    }

    private static void spiderVenom()
    {
        if (!hit()) return;
        dealDmg(2);
        Buff venom = new Buff(0, -3, 0, -3, 2);
        target.addBuff(venom);
    }

    private static void rockBasic()
    {
        if (!hit()) return;
        dealDmg(7);
    }

    private static void rockStun()
    {
        if (!hit()) return;
        dealDmg(4);
        Effect stun = new Effect(EffectName.STUN, 1, 0, true);
        target.addEffect(stun);
        target.transform.GetChild(0).GetComponent<SkillEffects>().setStunIcon(true);
    }

    private static void rockShield()
    {
        Effect shield = new Effect(EffectName.SHIELD, 1, 0, false);
        caster.addEffect(shield);
    }

    private static void zombieHeal()
    {
        Effect healing = new Effect(EffectName.HEAL, 2, -3, false);
        caster.addEffect(healing);
        caster.transform.GetChild(0).GetComponent<SkillEffects>().setHealIcon(true);
    }

    private static void zombieBasic()
    {
        if (!hit()) return;
        dealDmg(2);
    }

    private static void clownBasic()
    {
        if (!hit()) return;
        dealDmg(4);
    }

    private static void witchBasic()
    {
        if (!hit()) return;
        dealDmg(4);
    }

    private static void witchHeal()
    {
        List<Stats> aliveEnemies = Turn.control.getAliveEnemies();

        foreach(Stats s in aliveEnemies)
        {
            s.healthChange(3);
        }
    }

    private static void witchSummon()
    {
        LoadCharacters.control.summon(EnemyType.TORNADO, 1);
    }

    private static void ratBasic()
    {
        if (!hit()) return;
        dealDmg(2);
    }

    private static void ratSummonRats()
    {
        LoadCharacters.control.summon(EnemyType.RAT, 1);
    }

    private static void tornadoBasic()
    {
        foreach (GameObject player in Turn.control.alivePlayers)
        {
            target = player.GetComponent<Stats>();
            if (!hit()) return;
            dealDmg(3);
        }
        caster.healthChange(-1);
    }

    private static void tornadoBuff()
    {   
        foreach (Stats s in Turn.control.getAliveEnemies())
        {
            s.addBuff(new Buff(3, 0, 0, 0, 2));
        }
        caster.healthChange(-1);
    }

    private static void skipTurn()
    {
        Debug.Log(caster.gameObject.name + " skipped turn");
    }
}
