using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public int id;
    public string name;
    public string whatWantInTeammates; //docelowo mo?e enum
    public string description;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public Character(int id, string name, string whatWantInTeamMates, string description, Dictionary<string, int> stats)
    {
        this.id = id;
        this.name = name;
        this.whatWantInTeammates = whatWantInTeamMates;
        this.description = description;
        icon = Resources.Load<Sprite>("Sprites/" + name);
        this.stats = stats;
    }

    public Character(Character character)
    {
        this.id = character.id;
        this.name = character.name;
        this.whatWantInTeammates = character.whatWantInTeammates;
        this.description = character.description;
        icon = Resources.Load<Sprite>("Sprites/" + name);
        this.stats = character.stats;
    }

    public Character(Hero hero)
    {
        this.id = hero.id;
        this.name = hero.name;
        this.whatWantInTeammates = hero.whatWantInTeammates;
        this.description = hero.description;
        this.icon = hero.icon;
    }
}
