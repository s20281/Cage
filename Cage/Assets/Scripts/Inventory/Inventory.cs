using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemsInInventory = new List<Item>();
    public List<Character> charactersInInventory = new List<Character>();
    public ItemDatabase itemDatabase;
    public TeamDatabase teamDatabase;

    public static Inventory control;

    public UIInventory inventoryUI;
    public TeamContainer teamContainer;

    public ScriptableInventory inventory;
    private GameObject GM;
    public GameObject itemsList;


    void Start()
    {
        control = this;


        GM = GameObject.FindGameObjectWithTag("GM");

        for (var i = 0; i < teamContainer.getUIItemSize(); i++)
        {
            //charactersInInventory.Add(teamDatabase.GetCharacter(0));
            if (GM.GetComponent<Team>().heroes[i] != null)
            {
                //charactersInInventory.Add(new Character(GM.GetComponent<Team>().heroes[i]));
                teamContainer.UpdateSlot(i, new Character(GM.GetComponent<Team>().heroes[i]));
                charactersInInventory.Add(teamDatabase.GetCharacter(1));
            }
            else
            {
                charactersInInventory.Add(teamDatabase.GetCharacter(0));
            }

        }


        foreach (ScriptableItem item in GM.GetComponent<Inventory2>().items)   // to si? wykonuje 2 razy z jakiego? powodu
        {
            AddItem(item.name);
        }


        //Character playerCharacter = teamDatabase.GetCharacter("player");
        //AddItem(playerCharacter.name);

        //var slotIndex = TeamContainer.control.FindIndexOfCharacter(playerCharacter);
        //Debug.Log(slotIndex);
        //charactersInInventory[slotIndex] = playerCharacter;

        /*foreach (var a in GetAllCharacters())
        {

            Debug.Log(a.name);
        }*/
    }



    public void AddItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        Character characterToAdd = teamDatabase.GetCharacter(id);

        if (itemToAdd != null)
        {
            inventoryUI.AddNewItem(itemToAdd);
            itemsInInventory.Add(itemToAdd);
        }

        if (characterToAdd != null)
        {
            var index = 0;

            foreach (var a in GetAllCharacters())
            {
                if (a.name == "blank")
                {
                    AssignmentAction(charactersInInventory[index], characterToAdd);
                    break;
                }
                index++;
            }

            teamContainer.AddNewCharacter(characterToAdd);
            GetAllCharacters()[index] = characterToAdd;



        }


    }

    public void AddItem(string name)
    {
        Item itemToAdd = itemDatabase.GetItem(name);
        Character characterToAdd = teamDatabase.GetCharacter(name);


        if (itemToAdd != null)
        {
            inventoryUI.AddNewItem(itemToAdd);
            itemsInInventory.Add(itemToAdd);
        }

        if (characterToAdd != null)
        {
            int index = 0;

            foreach (var a in GetAllCharacters())
            {
                Debug.Log(a.name);
                if (a.name == "blank")
                {
                    AssignmentAction(charactersInInventory[index], characterToAdd);
                    break;
                }
                index++;
            }


            teamContainer.AddNewCharacter(characterToAdd);
            GetAllCharacters()[index] = characterToAdd;

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

    public Character FindCharacter(int id)
    {
        return charactersInInventory.Find(character => character.id == id);
    }

    public Character FindCharacter(string name)
    {
        return charactersInInventory.Find(character => character.name == name);
    }

    public void RemoveItem(int id)
    {
        Item item = FindItem(id);
        Character character = FindCharacter(id);

        if (item != null)
        {
            itemsInInventory.Remove(item);
            inventoryUI.RemoveItem(item);

        }

        if (character != null)
        {
            charactersInInventory.Remove(character);
            teamContainer.RemoveCharacter(character);

        }
    }

    public void RemoveItem(string name)
    {
        Item item = FindItem(name);
        Character character = FindCharacter(name);

        if (item != null)
        {
            itemsInInventory.Remove(item);
            inventoryUI.RemoveItem(item);
            GM.GetComponent<Inventory2>().removeItem(item);

        }

        if (character != null)
        {

            charactersInInventory.Remove(character);
            teamContainer.RemoveCharacter(character);

        }

    }

    public void RemoveAndSpawnItem(string name)
    {
        Item item = FindItem(name);
        Character character = FindCharacter(name);

        if (item != null)
        {
            itemsInInventory.Remove(item);
            GM.GetComponent<Inventory2>().removeItem(item);

            GameObject player = GameObject.FindGameObjectWithTag("WalkPlayer");
            Vector3 playerCordinates = player.transform.position;
            Vector3 itemPosition = new Vector3(playerCordinates.x + 3, playerCordinates.y, playerCordinates.z);

            if (itemsList.transform.Find(item.name))
            {
                GameObject neededItem = itemsList.transform.Find(item.name).gameObject;
                neededItem.transform.position = itemPosition;
                neededItem.SetActive(true);
                neededItem.transform.Find("interactionTag").gameObject.SetActive(false);

            }

        }

        if (character != null)
        {

            charactersInInventory.Remove(character);
            teamContainer.RemoveCharacter(character);

        }


    }

    public List<Item> GetAllItems()
    {
        return itemsInInventory;
    }

    public bool hasCompanion()
    {
        if (charactersInInventory.Count > 0)
            return true;
        else
            return false;
    }

    public List<Character> GetAllCharacters()
    {
        return charactersInInventory;
    }

    public static void AssignmentAction(Character characterToChange, Character character)
    {
        characterToChange = character;
    }



}
