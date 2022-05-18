using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public static int heroesLimit = 8;
    public static int heroesCount;
    public Hero[] heroes;
    public Hero main;
    public Hero temp;

    public Hero h1;
    public Hero h2;
    public Hero h3;

    public Dictionary<string, Hero> heroMapping = new Dictionary<string, Hero>();

    private void Awake()
    {
        heroes = new Hero[8];
        heroesCount = 0;
        addHero(main);

        heroMapping.Add(h1.name, h1);
        heroMapping.Add(h2.name, h2);
        heroMapping.Add(h3.name, h3);
    }

    public void addHero(Hero hero)
    {
        for(int i = 0; i < 8; i++)
        {
            if (heroes[i] == null)
            {
                heroes[i] = hero;
                hero.health = hero.maxHealth;
                heroesCount++;

                break;
            }
        } 
    }

    public void removeHero(Hero hero)
    {
        for (int i = 0; i < 8; i++)
        {
            if (heroes[i].name == hero.name)
            {
                heroes[i] = null;
                heroesCount--;
                break;
            }
        }
    }

    public void shareExp(int exp)
    {
        for (int i = 0; i < 8; i++)
        {
            if (heroes[i] != null)
            {
                heroes[i].addExp(exp / heroesCount);
            }
        }
    }
}
