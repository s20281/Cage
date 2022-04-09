using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory")]
public class ScriptableInventory : ScriptableObject
{
    public Dictionary<string, ScriptableItem> inventory = new Dictionary<string, ScriptableItem>();
    public int count;


    public ScriptableItem getItem(string name)
    {
        return inventory[name];
    }

    public void addItem(ScriptableItem item)
    {
        if (item.singleUse && inventory.ContainsKey(item.name))
            inventory[item.name].count++;
        else
        {
            inventory.Add(item.name, item);
        }
        count = inventory.Count;

    }

    public void removeItem(ScriptableItem item)
    {
        if (item.singleUse && inventory.ContainsKey(item.name) && inventory[item.name].count > 1)
            inventory[item.name].count--;
        else
        {
            inventory.Remove(item.name);
        }
        count = inventory.Count;
    }
}
