using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Skill {NONE, SKIP, SWORD, POTION }


public class UseSkill : MonoBehaviour
{
    public int activeSkill;
    public Turn turn;
    public Stats skillUser;
    public Stats target;
    public bool hasTarget;

    public Skill actSkill;
    private Dictionary<Skill, Action> skillMapping;

    void Start()
    {
        activeSkill = 0;
        actSkill = Skill.NONE;
        GameEventSystem.Instance.OnMouseOverEnemy += setTarget;
        GameEventSystem.Instance.OnMouseExitEnemy += unsetTarget;
        GameEventSystem.Instance.OnItemSelect += selectItem;
        hasTarget = false;

        skillMapping = new Dictionary<Skill, Action>
        {
            {Skill.SWORD, () => {sword();} },
            {Skill.POTION, () => {potion(); } },
            {Skill.SKIP, () => {skipTurn(); } }
        };
}

    void Update()
    {
        if (hasTarget && actSkill != Skill.NONE && Input.GetMouseButtonDown(0))
        {
            skillUser = turn.getActivePlayer();
            skillMapping[actSkill]();

            actSkill = Skill.NONE;

            GameEventSystem.Instance.SetSkillUse();
            Destroy(skillUser.queueIcon);
        }
    }

    public void setTarget(Stats stats)
    {
        if(!stats.isDead)
        {
            hasTarget = true;
            target = stats;
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

    public void selectItem(Skill skill)
    {
        actSkill = skill;
        Debug.Log(skill);
    }

    private void simpleAtack()
    {
        float modificator = skillUser.aim - target.dodge;
        float roll = UnityEngine.Random.Range(-5, 5);
        GameObject enemy = target.gameObject;
        GameObject player = skillUser.gameObject;

        if (roll + modificator < 0)
        {
            // Miss
            Debug.Log("Miss");
            
            enemy.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("DODGE", Color.green);
            player.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("MISS", Color.yellow);
            return;
        }

        int damage = 3 + skillUser.strength;
        enemy.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect(damage.ToString(), Color.red);
        target.onHealthChange(-damage);
        Debug.Log("Player attacks for " + damage);
    }

    private void sword()
    {
        int baseDmg = 3;
        simpleAtack();
    }

    private void potion()
    {
        int roll = UnityEngine.Random.Range(5, 7);
        target.onHealthChange(roll);
        GameEventSystem.Instance.SetPositiveSkillUse(target);
        Debug.Log("Player heals for " + roll);
    }

    private void protection()
    {
        target.dodge += 3;
        GameEventSystem.Instance.SetPositiveSkillUse(target);

        Debug.Log("Player heals increases he's dodge for " + 3);
    }

    private void skipTurn()
    {
        Debug.Log("Skipped turn");
    }

}
