using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Sprite dead;
    public bool isDead;
    public bool queued = false;
    public GameObject queueIcon;
    public List<Effect> effectsList = new List<Effect>();

    private GameObject GM;

    void Start()
    {
        isDead = false;
        GM = GameObject.FindGameObjectWithTag("GM");
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

    public void onHealthChange(int change)
    {
        health += change;
        if (health <= 0)
        {
            health = 0;
            onDead();
        }
            
        if (health > maxHealth)
            health = maxHealth;

        hpBar.changeHealth(change);
    }

    public void onDead()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = dead;
        isDead = true;
        
        if(gameObject.CompareTag("Enemy"))
        {
            GameEventSystem.Instance.SetEnemyDies();
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
        gameObject.GetComponent<SpriteRenderer>().sprite = hero.icon;
    }
}