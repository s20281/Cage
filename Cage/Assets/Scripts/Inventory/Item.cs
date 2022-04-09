using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public string character;
    public string description;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public Skill skill;

    public Item(int id, string name, string character, string description, Dictionary<string, int> stats, Skill skill)
    {
        this.id = id;
        this.name = name;
        this.character = character;
        this.description = description;
        icon = Resources.Load<Sprite>("Sprites/" + name);
        this.stats = stats;
        this.skill = skill;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.name = item.name;
        this.character = item.character;
        this.description = item.description;
        icon = Resources.Load<Sprite>("Sprites/" + name);
        this.stats = item.stats;
        this.skill = item.skill;
    }
}
