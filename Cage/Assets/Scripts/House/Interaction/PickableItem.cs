using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickableItem : MonoBehaviour
{
    public static PickableItem control;
    Inventory inventory;

    public ScriptableItem item;
    public List<ScriptableItem> startingItems = new List<ScriptableItem>();
    public Hero hero;

    void Awake()
    {
        control = this;
    }

    private void Start()
    {
        if(gameObject.CompareTag("Item"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = item.icon;
            this.gameObject.name = item.name;
        }
        else if(gameObject.CompareTag("Hero"))
        {

        }
        
    }


    public void PickItem(Inventory inventory)
    {
        Debug.Log("Picked " + gameObject.name);
        inventory.AddItem(gameObject.name);

        //inventory2{
        GameObject gm = GameObject.FindGameObjectWithTag("GM");
        if (gameObject.CompareTag("Item"))
        {
            
            gm.gameObject.GetComponent<Inventory2>().inv.addItem(item); //testowe
            gm.gameObject.GetComponent<Inventory2>().items.Add(item);
        }
        else if (gameObject.CompareTag("Hero"))
        {
            gm.gameObject.GetComponent<Team>().addHero(hero);

            foreach(ScriptableItem item in startingItems)
            {
                gm.gameObject.GetComponent<Inventory2>().items.Add(item);
                //inventory.AddItem(ItemDatabase.control.GetItem(item.name));
            }
        }
        //}
        gameObject.SetActive(false);

        this.GetComponent<ObjectsManager>().setOff();
    }
}
