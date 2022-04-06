using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableItem : MonoBehaviour
{
    public GameObject itemToUnlockDoor;


    public void OpenIt(Inventory inventory)
    {

        if (itemToUnlockDoor != null)
        {
            if (inventory.FindItem(itemToUnlockDoor.name)!=null)
            {
                inventory.RemoveItem(itemToUnlockDoor.name);
                doorOpening();

            }

        }
        else
        {
            doorOpening();
        }


    }

    public void doorOpening()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        gameObject.SetActive(false);
        gameObject.GetComponent<Collider2D>().enabled = false;

        this.GetComponent<ObjectsManager>().setOff();
    }
}
