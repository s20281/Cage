using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfslots = 15;

    private void Awake()
    {
        for(int i = 0; i< numberOfslots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
            
        }
 
    }

    public void UpdateSlot(int slot, Item item)
    {
       
        uIItems[slot].UpdateItem(item, null);
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
