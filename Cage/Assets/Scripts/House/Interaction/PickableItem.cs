using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickableItem : MonoBehaviour
{
    public static PickableItem control;
    Inventory inventory;

    void Awake()
    {
        control = this;
    }


    public void PickItem(Inventory inventory)
    {
        Debug.Log("Pick");
        inventory.AddItem(gameObject.name);
        gameObject.SetActive(false);
        
        
    }
}
