using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryForCombat : MonoBehaviour
{
    Inventory inv;
    List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfslots = 5;
    private List<Item> itemsInInventory;
    public static InventoryForCombat control;
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

    void Start()
    {
        inv = Inventory.control;
        Item item = inv.FindItem(1);
        itemsInInventory = inv.GetAllItems();     
      

        foreach (var invItem in itemsInInventory)
        {
            if (invItem.character == this.gameObject.name)
            {
                Debug.Log(invItem.name);
                AddNewItem(invItem);
            }
        }

        

    }


    private void Update()
    {

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
