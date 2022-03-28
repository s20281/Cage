using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemsInInventory = new List<Item>();
    public List<Item> charactersInInventory = new List<Item>();
    public ItemDatabase itemDatabase;
    public TeamDatabase teamDatabase;
	
	public static Inventory control;

    public UIInventory inventoryUI;
    public TeamContainer teamContainer;


    void Awake(){
		control = this;
	}


    public void AddItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        Item characterToAdd = teamDatabase.GetItem(id);
        if(itemToAdd != null)
        {
            inventoryUI.AddNewItem(itemToAdd);
            itemsInInventory.Add(itemToAdd);
        }
    
        if(characterToAdd != null)
        {
            charactersInInventory.Add(characterToAdd);
            teamContainer.AddNewItem(characterToAdd);
            
        }
        

    }

    public void AddItem(string name)
    {
        Item itemToAdd = itemDatabase.GetItem(name);
        Item characterToAdd = teamDatabase.GetItem(name);

        if (itemToAdd != null)
        {
            inventoryUI.AddNewItem(itemToAdd);
            itemsInInventory.Add(itemToAdd);
        }
        if (characterToAdd != null)
        {
            teamContainer.AddNewItem(characterToAdd);
            charactersInInventory.Add(characterToAdd);
        }

    }

    public Item FindItem(int id)
    {
        return itemsInInventory.Find(item => item.id == id);

    }

    public Item FindItem(string name)
    {
        return itemsInInventory.Find(item => item.name == name);

    }

    public Item FindCharacter(int id)
    {
        return charactersInInventory.Find(item => item.id == id);
    }

    public Item FindCharacter(string name)
    {
        return charactersInInventory.Find(item => item.name == name);
    }

    public void RemoveItem (int id)
    {
        Item item = FindItem(id);
        Item character = FindCharacter(id);

        if(item != null)
        {
            itemsInInventory.Remove(item);
            inventoryUI.RemoveItem(item);

        }

        if (character != null)
        {
            charactersInInventory.Remove(character);
            teamContainer.RemoveItem(character);

        }




    }

    public void RemoveItem(string name)
    {
        Item item = FindItem(name);
        Item character = FindCharacter(name);

        if (item != null)
        {
            itemsInInventory.Remove(item);
            inventoryUI.RemoveItem(item);

        }

        if (character != null)
        {
            charactersInInventory.Remove(character);
            teamContainer.RemoveItem(character);

        }


    }

    public List<Item> GetAllItems()
    {
         return itemsInInventory;
    }

    public List<Item> GetAllCharacters()
    {
        return charactersInInventory;
    }




}
