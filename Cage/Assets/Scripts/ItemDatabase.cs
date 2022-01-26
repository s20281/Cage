using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
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
            new Item(0, "potion", "Make you healthy.", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }),

            new Item(1, "sword", "Fight! Fight!", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }),
            new Item(2, "key", "open the door with it!", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }),


        };
    }
}
