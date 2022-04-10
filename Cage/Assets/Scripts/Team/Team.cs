using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public static int heroesLimit = 8;
    public Hero[] heroes;
    public Hero main;
    public Hero temp;

    private void Start()
    {
        heroes = new Hero[8];
        addHero(main);
    }

    public void addHero(Hero hero)
    {
        for(int i = 0; i < 8; i++)
        {
            if (heroes[i] == null)
            {
                heroes[i] = hero;
                break;
            }
        } 
    }
}
