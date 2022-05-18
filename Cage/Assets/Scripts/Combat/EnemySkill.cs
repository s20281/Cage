using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemySkill
{
    public enum EnemyType { SHADOW, ROCK, SPIDER}


    public static Dictionary<EnemyType, List<Action>> skillMaping;

    static Stats caster;
    static Stats target;

    public static void Start()
    {
        skillMaping = new Dictionary<EnemyType, List<Action>>();

        List<Action> spiderSkills = new List<Action>();
        spiderSkills.Add(spiderWeb);
        spiderSkills.Add(spiderVenom);
        skillMaping.Add(EnemyType.SPIDER, spiderSkills);


    }

    public static void UseSkill(Stats c, Stats t)
    {
        caster = c;
        target = t;

        if (!hit())
            return;

        EnemyType enemyType = EnemyType.SPIDER; // NA SZTYWNO

        int rand = UnityEngine.Random.Range(0, 10) % skillMaping[enemyType].Count;

        skillMaping[enemyType][rand]();
    }


    private static bool hit()
    {
        int aim = caster.aim;
        int dodge = target.dodge;
        int diff = aim - dodge;

        int score = UnityEngine.Random.Range(-10, 10) + diff;

        if(score < 0)
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

    private static void spiderWeb()
    {
        dealDmg(2);
        Buff web = new Buff(-3, 0, -3, 0, 2);
        target.addBuff(web);
    }

    private static void spiderVenom()
    {
        dealDmg(2);
        Buff venom = new Buff(0, -3, 0, -3, 2);
        target.addBuff(venom);
    }




}
