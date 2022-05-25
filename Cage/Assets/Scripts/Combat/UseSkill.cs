using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Skill {NONE, SKIP, SWORD, POTION, HAMMER, BASEBALL, SHURIKEN, FLASHBANG, KATANA, LASERGUN, FORCEFIELD, SHIELD, BACTA}

public enum TargetType {ENEMY, FRIEND, OBJECT}




public class UseSkill : MonoBehaviour
{
    public static UseSkill control;
    //public int activeSkill;
    public Turn turn;
    public Stats skillUser;
    public Stats target;
    public bool hasTarget;
    public Skill actSkill;
    public TargetType targetType;
    public Item actItem;
    private Dictionary<Skill, Action> skillMapping;
    private GameObject GM;

    private void Awake()
    {
        control = this;
    }

    void Start()
    {
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
            {Skill.SKIP, () => {skipTurn(); } },
            {Skill.SHURIKEN, () => {shuriken(); } },
            {Skill.KATANA, () => {katana(); } },
            {Skill.FLASHBANG, () => {flashbang(); } },
            {Skill.SHIELD, () => {shield(); } },
            {Skill.LASERGUN, () => {laserGun(); } },
            {Skill.BACTA, () => {bactaTank(); } }
        };

        GM = GameManager.getGameObject();
    }

    void Update()
    {
        if (hasTarget && actSkill != Skill.NONE && properTarget() && Input.GetMouseButtonDown(0))
        {
            Use();
        }
    }

    public void Use()
    {
        skillUser = turn.getActivePlayer();
        skillMapping[actSkill]();

        if (actItem.name == "potion")
            GM.GetComponent<Inventory2>().removeItem(actItem);


        actSkill = Skill.NONE;

        GameEventSystem.Instance.SetSkillUse();
        Destroy(skillUser.queueIcon);
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

    public bool properTarget()
    {
        if (targetType == TargetType.ENEMY && target.gameObject.CompareTag("Enemy"))
            return true;
        if (targetType == TargetType.FRIEND && target.gameObject.CompareTag("Player"))
            return true;
        return false;
    }

    public void selectItem(Item item)
    {
        actSkill = item.skill;
        targetType = item.targetType;
        actItem = item;
    }

    private void displayEffect(GameObject go, string text, Color color)
    {
        go.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect(text, color);
    }

    private bool simpleAtack(int baseDmg, bool dealsDamage = true)
    {
        float modificator = skillUser.aim - target.dodge;
        float roll = UnityEngine.Random.Range(-5, 5);
        GameObject enemy = target.gameObject;
        GameObject player = skillUser.gameObject;

        if (roll + modificator < 0)
        {
            Debug.Log("Miss");

            displayEffect(enemy, "DODGE", Color.green);
            displayEffect(player, "MISS", Color.yellow);
            return false;
        }

        int damage = 0;
       
        if(dealsDamage)
            damage = baseDmg + skillUser.strength;
        target.healthChange(-damage);
 
        Debug.Log(player.gameObject.name + " attacks for " + damage);
        return true;
    }

    private void stun(int turns)
    {
        Effect stun = new Effect(EffectName.STUN, turns, 0, true);
        target.addEffect(stun);
    }
    private void bleeding(int turns, int damage)
    {
        Effect bleeding = new Effect(EffectName.BLEEDING, turns, damage, false);
        target.addEffect(bleeding);
    }
    private void healing(int turns, int heal)
    {
        Effect healing = new Effect(EffectName.BLEEDING, turns, -heal, false);
        target.addEffect(healing);
    }
    private void shield(int turns)
    {
        Effect shield = new Effect(EffectName.SHIELD, turns, 0, false);
        target.addEffect(shield);
    }
    private void addBuff(Stats s, int speed, int strength, int dodge, int aim, int turns)
    {
        Buff buff = new Buff(speed, strength, dodge, aim, turns);
        s.addBuff(buff);
    }


    private void sword()
    {
        simpleAtack(3);
    }

    private void hammer()
    {
        if(simpleAtack(1) && !target.isDead)
            stun(1);
    }

    private void baseball()
    {
        if (simpleAtack(1) && !target.isDead)
            bleeding(2, 2);
    }

    private void potion()
    {
        int roll = UnityEngine.Random.Range(5, 7);
        target.healthChange(roll);
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

    private void shuriken()
    {
        simpleAtack(5);

    }
    private void katana()
    {
        if (simpleAtack(3) && !target.isDead)
            bleeding(2, 2);
    }
    private void flashbang()
    {
        if (simpleAtack(0, false) && !target.isDead)
            stun(2);
    }
    private void forceField()
    {
        Effect shield = new Effect(EffectName.SHIELD, 1, 0, false);
        target.addEffect(shield);
    }

    private void laserGun()
    {
        if (simpleAtack(3) && !target.isDead)
        {
            addBuff(target,0, 0, 0, -2, 2);
        }
    }

    private void bactaTank()
    {
        Effect bacta = new Effect(EffectName.SHIELD, 2, -3, false);
        target.addEffect(bacta);
    }


    private void shield()
    {
        Effect protect = new Effect(EffectName.PROTECTION, 2, 0, false);
        target.addEffect(protect);
    }

}
