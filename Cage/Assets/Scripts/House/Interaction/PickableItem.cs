using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickableItem : MonoBehaviour
{
    public static PickableItem control;
    Inventory inventory;

    public ScriptableItem item;
    public Hero hero;

    void Awake()
    {
        control = this;
    }

    private void Start()
    {
        if(gameObject.CompareTag("Item"))// TODO zmieniæ heroes ¿eby nie byli pickableItem
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
        if (gameObject.CompareTag("Item")) // TODO zmieniæ heroes ¿eby nie byli pickableItem
        {
            
            gm.gameObject.GetComponent<Inventory2>().inv.addItem(item);
            gm.gameObject.GetComponent<Inventory2>().items.Add(gameObject.name);  
        }
        else if (gameObject.CompareTag("Hero"))
        {
            gm.gameObject.GetComponent<Team>().addHero(hero);
        }
        //}
        gameObject.SetActive(false);

        this.GetComponent<ObjectsManager>().setOff();
    }
}
