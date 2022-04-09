using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory")]
public class ScriptableInventory : ScriptableObject
{
    public Dictionary<string, GameObject> inventory = new Dictionary<string, GameObject>();
    public int count;


    public GameObject getItem(string name)
    {
        return inventory[name];
    }

    public void addItem(GameObject item)
    {
        inventory.Add(item.name, item);
        count = inventory.Count;
    }

    public void removeItem(GameObject item)
    {
        inventory.Remove(item.name);
        count = inventory.Count;
    }
}
