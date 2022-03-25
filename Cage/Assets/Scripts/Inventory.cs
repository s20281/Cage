using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemsInInventory = new List<Item>();
    public ItemDatabase itemDatabase;
	
	 public static Inventory control;

    public UIInventory inventoryUI;
    
	
	void Awake(){
		control = this;
	}

    public void AddItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        inventoryUI.AddNewItem(itemToAdd);
        itemsInInventory.Add(itemToAdd);
        

    }

    public void AddItem(string name)
    {
        Item itemToAdd = itemDatabase.GetItem(name);
        inventoryUI.AddNewItem(itemToAdd);
        itemsInInventory.Add(itemToAdd);

    }

    public Item FindItem(int id)
    {
        return itemsInInventory.Find(item => item.id == id);

    }

    public Item FindItem(string name)
    {
        return itemsInInventory.Find(item => item.name == name);

    }

    public void RemoveItem (int id)
    {
        Item item = FindItem(id);

        if(item != null)
        {
            itemsInInventory.Remove(item);
            inventoryUI.RemoveItem(item);

        }
              

    }

    public void RemoveItem(string name)
    {
        Item item = FindItem(name);

        if (item != null)
        {
            itemsInInventory.Remove(item);
            inventoryUI.RemoveItem(item);

        }


    }

    public List<Item> GetAllItems()
    {
         return itemsInInventory;
    }





}
