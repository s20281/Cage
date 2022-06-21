using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventSystemMap : MonoBehaviour
{
    private static GameEventSystemMap instance;

    public static GameEventSystemMap Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameEventSystemMap>();
            return instance;
        }
    }

    public UIInventory uIInventory;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if(PlayerPrefs.GetInt("loadGame") == 1)
        {
            SaveSystem.control.loadGame();
            PlayerPrefs.SetInt("loadGame", 0);

            

        }

        if (StaticClass.loadScene)
        {
            Debug.Log("load scene");
        }

        else
        {
            Debug.Log("New Run");
            PlayerPrefs.DeleteAll();
        }
        StaticClass.loadScene = true;
    }

    public event Action<Hero> OnHeroSelect;

    public void DelteItemFromInventory()
    {
        if (UIItem.control.selectedItem)
        {
            Item item = UIItem.control.selectedItem.item;

            if(item != null)
            {
                UIItem.control.RemoveSelectedItem();
                Inventory.control.RemoveAndSpawnItem(item.name);
            }
        }
    }

    public void SetHeroSelect(Hero hero)
    {
        OnHeroSelect.Invoke(hero);
    }

    private void Start()
    {
        PlayerPrefs.SetString("level", SceneManager.GetActiveScene().name);

        GameObject GM = GameObject.FindGameObjectWithTag("GM");

        List<ScriptableItem> items = GM.GetComponent<Inventory2>().items;

        foreach (ScriptableItem i in items)
        {
            uIInventory.AddNewItem(new Item(i));
        }
    }
}
