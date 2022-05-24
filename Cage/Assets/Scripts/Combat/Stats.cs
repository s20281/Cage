using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType {Hero, Enemy}

public class Stats : MonoBehaviour
{
    public Hero hero;
    public int maxHealth;
    public int health;
    public int speed;
    public int strength;
    public int dodge;
    public int aim;
    public HpBar hpBar;
    public CharacterType characterType;
    public int expForKill;
    public int mentalHealth;

    public Sprite dead;
    public bool isDead;
    public bool queued = false;
    public GameObject queueIcon;
    public List<Effect> effectsList = new List<Effect>();
    public List<Buff> buffsList = new List<Buff>();

    private GameObject GM;

    void Start()
    {
        isDead = false;
        GM = GameObject.FindGameObjectWithTag("GM");

        if (gameObject.CompareTag("Player"))
            characterType = CharacterType.Hero;
        else
            characterType = CharacterType.Enemy;
    }

    void OnMouseOver()
    {
        GameEventSystem.Instance.SetMouseOverEnemy(this);

        if (gameObject.CompareTag("Enemy"))
        {
            GameEventSystem.Instance.SetEnemyStatsActive(true);
        }
        else if (gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.SetPlayerStatsActive(true);
        }
    }

    void OnMouseExit()
    {
        GameEventSystem.Instance.SetMouseExitEnemy(this);

        if (gameObject.CompareTag("Enemy"))
        {
            GameEventSystem.Instance.SetEnemyStatsActive(false);
        }
        else if (gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.SetPlayerStatsActive(false);
        }
    }

    public void healthChange(int change)
    {
        health += change;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
            
        if (health > maxHealth)
            health = maxHealth;

        if(characterType == CharacterType.Hero)
            hero.health = health;

        hpBar.changeHealth(change);
    }

    public void Die()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = dead;
        isDead = true;
        
        if(characterType == CharacterType.Enemy)
        {
            GameEventSystem.Instance.SetEnemyDies();
            GM.GetComponent<Team>().shareExp(expForKill);
        }
        else if (gameObject.CompareTag("Player"))
        {
            GameEventSystem.Instance.SetPlayerDies(this.gameObject);
            GM.GetComponent<Team>().removeHero(hero);
        }
        Destroy(queueIcon);
        this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
    }

    public void addEffect(Effect effect)
    {
        effectsList.Add(effect);
    }

    public void setStats(Hero hero)
    {
        this.hero = hero;
        maxHealth = hero.maxHealth;
        health = hero.health;
        speed = hero.speed;
        strength = hero.strength;
        dodge = hero.dodge;
        aim = hero.accuracy;
        mentalHealth = hero.mentalHealth;
        gameObject.GetComponent<SpriteRenderer>().sprite = hero.icon;
    }

    public void addBuff(Buff buff)
    {
        buffsList.Add(buff);
        buff.apply(this);
    }
}