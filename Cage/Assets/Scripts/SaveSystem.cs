using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem control;

    public Hero main;
    public Hero ninja;
    public Hero astro;
    public Hero hulk;

    public Inventory2 inventory;
    public Inventory playerInventory;

    private void Awake()
    {
        control = this;
    }

    public void newGame()
    {
        Debug.Log("Started New Game");

        //main.mentalHealth = <DEFAULT MENTAL?>
        main.level = 1;
        main.exp = 0;
        main.levelUpPoints = 0;
        main.maxHealth = 20;
        main.health = 20;
        main.accuracy = 7;
        main.dodge = 5;
        main.speed = 5;
        main.strength = 5;

        ninja.level = 1;
        ninja.exp = 0;
        ninja.levelUpPoints = 0;
        ninja.maxHealth = 15;
        ninja.health = 15;
        ninja.accuracy = 8;
        ninja.dodge = 4;
        ninja.speed = 7;
        ninja.strength = 2;
        PlayerPrefs.SetInt("Ninja", 0);

        astro.level = 1;
        astro.exp = 0;
        astro.levelUpPoints = 0;
        astro.maxHealth = 20;
        astro.health = 20;
        astro.accuracy = 7;
        astro.dodge = 4;
        astro.speed = 3;
        astro.strength = 3;
        PlayerPrefs.SetInt("Astronaut", 0);

        hulk.level = 1;
        hulk.exp = 0;
        hulk.levelUpPoints = 0;
        hulk.maxHealth = 30;
        hulk.health = 30;
        hulk.accuracy = 5;
        hulk.dodge = 3;
        hulk.speed = 3;
        hulk.strength = 6;
        PlayerPrefs.SetInt("Hulk", 0);

        for (int i = 0; i < 15; i++)
        {
            PlayerPrefs.SetString("inventory." + i, "");
        }
    }

    public void loadGame()
    {
        Debug.Log("Loaded Game");
        playerInventory = GameObject.FindGameObjectWithTag("WalkPlayer").GetComponent<Inventory>();

        int loadNinja = PlayerPrefs.GetInt("Ninja");
        if (loadNinja == 1)
        {
            gameObject.GetComponent<Team>().addHero(ninja);
            playerInventory.AddItem(ninja.name);
        }
            

        int loadHulk = PlayerPrefs.GetInt("Hulk");
        if (loadHulk == 1)
        {
            gameObject.GetComponent<Team>().addHero(hulk);
            playerInventory.AddItem(hulk.name);
        }
            

        int loadAstro = PlayerPrefs.GetInt("Astronaut");
        if (loadAstro == 1)
        {
            gameObject.GetComponent<Team>().addHero(hulk);
        }
            


        for (int i = 0; i < 15; i++)
        {
            string itemName = PlayerPrefs.GetString("inventory." + i);
            if (itemName == "")
                continue;
            ScriptableItem item = gameObject.GetComponent<ScriptableItemMap>().mapping[itemName];
            inventory.items.Add(item);
            playerInventory.AddItem(gameObject.name);


        }
    }
}
