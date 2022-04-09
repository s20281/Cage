using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickableItem : MonoBehaviour
{
    public static PickableItem control;
    Inventory inventory;

    public ScriptableItem item;

    void Awake()
    {
        control = this;
    }

    private void Start()
    {
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = item.icon;
        //this.gameObject.name = item.name;
    }


    public void PickItem(Inventory inventory)
    {
        Debug.Log("Picked " + gameObject.name);
        inventory.AddItem(gameObject.name);
        GameObject player = GameObject.FindGameObjectWithTag("WalkPlayer");
        //Debug.Log(player.name);
        player.gameObject.GetComponent<Inventory>().inventory.addItem(gameObject);
        gameObject.SetActive(false);

        this.GetComponent<ObjectsManager>().setOff();
    }
}
