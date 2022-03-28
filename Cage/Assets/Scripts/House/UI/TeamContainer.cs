using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamContainer : MonoBehaviour
{
    Inventory inv;
    List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfslots;
    private List<Item> itemsInInventory;
    public static TeamContainer control;
    GameObject inventoryCanvas;

    private void Awake()
    {
        control = this;
        for (int i = 0; i < numberOfslots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }
    private void Update()
    {
        inv = Inventory.control;
        itemsInInventory = inv.GetAllCharacters();

        Debug.Log("Lista");

        foreach (var invItem in itemsInInventory)
        {
            
                Debug.Log(invItem.name);
                
            
        }
    }


    public void UpdateSlot(int slot, Item item)
    {
        uIItems[slot].UpdateItem(item);
      
    }
    public void AddNewItem(Item item)
    {

        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }
}
