using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string name)
    {
        return items.Find(item => item.name == name);
    }

    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item(0, "hulk", "Player", "Make you healthy.", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.POTION),

            new Item(1, "ninja", "Player", "Fight! Fight!", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.SWORD),

            new Item(2, "swordman", "Mutant","open the door with it!", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.NONE),


        };
    }
}
