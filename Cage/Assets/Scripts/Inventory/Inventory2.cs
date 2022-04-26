using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory2 : MonoBehaviour
{
    public ScriptableInventory inv;
    public ScriptableItem [] startingItems;
    public List<ScriptableItem> items;
    
    public void removeItem(Item item)
    {
        ScriptableItem itemToRemove = new ScriptableItem();
        foreach(var i in items)
        {
            if(i.name == item.name)
            {
                itemToRemove = i;
                break;
            }
        }
        items.Remove(itemToRemove);
        inv.removeItem(itemToRemove);
    }
}
