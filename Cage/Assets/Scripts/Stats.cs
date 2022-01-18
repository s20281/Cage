using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int speed;
    public int strength;
    public int dodge;
    public int aim;
    public HpBar hpBar;

    public Sprite dead;


    void Start()
    {

    }

    void Update()
    {

    }

    void OnMouseOver()
    {
        Debug.Log(gameObject.name);
        GameEventSystem.Instance.SetMouseOverEnemy(this);
    }
    void OnMouseExit()
    {
        GameEventSystem.Instance.SetMouseExitEnemy(this);
    }

    public void onHealthChange(int change)
    {
        health += change;
        if (health <= 0)
            onDead();
        hpBar.changeHealth(change);
    }

    public void onDead()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = dead;
    }
}