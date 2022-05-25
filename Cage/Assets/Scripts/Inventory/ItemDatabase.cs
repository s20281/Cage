using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public static ItemDatabase control;

    private void Awake()
    {
        control = this;
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
            new Item(0, "potion", "Main", "Make you healthy.", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.POTION, TargetType.FRIEND),

            new Item(1, "sword", "Main", "Fight! Fight!", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.SWORD, TargetType.ENEMY),

            new Item(2, "key", "Ninja","open the door with it!", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.SKIP, TargetType.ENEMY),

            new Item(3, "hammer", "Hulk","", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.HAMMER, TargetType.ENEMY),

            new Item(4, "baseball", "Ninja","", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.BASEBALL, TargetType.ENEMY),
            new Item(5, "katana", "Ninja","", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.KATANA, TargetType.ENEMY),
            new Item(6, "shuriken", "Ninja","", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.SHURIKEN, TargetType.ENEMY),
            new Item(7, "flashbang", "Ninja","", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.FLASHBANG, TargetType.ENEMY),
            new Item(8, "shield", "Hulk","", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.SHIELD, TargetType.FRIEND),
             new Item(9, "laserGun", "Astronaut","", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.LASERGUN, TargetType.ENEMY),
              new Item(10, "bacta", "Astronaut","", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }, Skill.BACTA, TargetType.FRIEND),


        };
    }
}
