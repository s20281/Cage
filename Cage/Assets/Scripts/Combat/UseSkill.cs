using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Skill {NONE, SKIP, SWORD, POTION, HAMMER, BASEBALL }


public class UseSkill : MonoBehaviour
{
    //public int activeSkill;
    public Turn turn;
    public Stats skillUser;
    public Stats target;
    public bool hasTarget;

    public Skill actSkill;
    private Dictionary<Skill, Action> skillMapping;

    void Start()
    {
        //activeSkill = 0;
        actSkill = Skill.NONE;
        GameEventSystem.Instance.OnMouseOverEnemy += setTarget;
        GameEventSystem.Instance.OnMouseExitEnemy += unsetTarget;
        GameEventSystem.Instance.OnItemSelect += selectItem;
        hasTarget = false;

        skillMapping = new Dictionary<Skill, Action>
        {
            {Skill.SWORD, () => {sword(); } },
            {Skill.POTION, () => {potion(); } },
            {Skill.HAMMER, () => {hammer(); } },
            {Skill.BASEBALL, () => {baseball(); } },
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

    //public void changeActiveSkillTo1()
    //{
    //    activeSkill = 1;
    //}
    //public void changeActiveSkillTo2()
    //{
    //    activeSkill = 2;
    //}
    //public void changeActiveSkillTo3()
    //{
    //    activeSkill = 3;
    //}
    //public void changeActiveSkillTo4()
    //{
    //    activeSkill = 4;
    //}

    public void selectItem(Skill skill)
    {
        actSkill = skill;
        //Debug.Log(skill);
    }

    private void displayEffect(GameObject gm, string text, Color color)
    {
        gm.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect(text, color);
    }

    private bool simpleAtack(int baseDmg)
    {
        float modificator = skillUser.aim - target.dodge;
        float roll = UnityEngine.Random.Range(-5, 5);
        GameObject enemy = target.gameObject;
        GameObject player = skillUser.gameObject;

        if (roll + modificator < 0)
        {
            // Miss
            Debug.Log("Miss");

            displayEffect(enemy, "DODGE", Color.green);
            displayEffect(player, "MISS", Color.yellow);
            return false;
        }

        int damage = baseDmg + skillUser.strength;
        displayEffect(enemy, damage.ToString(), Color.red);
        target.onHealthChange(-damage);
        Debug.Log("Player attacks for " + damage);
        return true;
    }

    private void stun(int turns)
    {
        Effect stun = new Effect(EffectName.STUN, turns, 0, true);
        target.addEffect(stun);
        displayEffect(target.gameObject, "STUN", Color.yellow);
    }

    private void bleeding(int turns, int damage)
    {
        Effect bleeding = new Effect(EffectName.BLEEDING, turns, damage, false);
        target.addEffect(bleeding);
    }

    private void sword()
    {
        simpleAtack(3);
    }

    private void hammer()
    {
        if(simpleAtack(1))
            stun(2);
    }

    private void baseball()
    {
        if (simpleAtack(1))
            bleeding(2, 2);
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
