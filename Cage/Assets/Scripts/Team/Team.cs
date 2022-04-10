using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public List<Hero> heroes;
    public Hero main;

    private void Start()
    {
        addHero(main);
    }

    public void addHero(Hero hero)
    {
        heroes.Add(hero);
    }
}
