using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroInventory : MonoBehaviour
{
    List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfslots = 6;
    private List<Item> itemsInInventory;
    private List<ScriptableItem> items;
    public static HeroInventory control;
    GameObject inventoryCanvas;
    private GameObject gm;

    public Hero activeHero;

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
        LoadItems();
    }

    private void LoadItems()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        items = gm.GetComponent<Inventory2>().items;

        foreach (var i in items)
        {
            if (i.character == activeHero.name)
            {
                Debug.Log("ITEM: " + i.name);
                AddNewItem(new Item(i));
            }
        }
    }

    public void ReloadItems()
    {
        for(int i = 0; i < numberOfslots; i++)
        {
            uIItems[i].UpdateItem(null, null);
        }

        UIItem.control.selectedItem.UpdateItem(null, null);

        var count = 0;
        foreach (var i in items)
        {
            if (i.character == activeHero.name)
            {
                uIItems[count].UpdateItem(null, null);
                count++;
            }
        }
        LoadItems();
    }

    public void UpdateSlot(int slot, Item item)
    {
        uIItems[slot].UpdateItem(item, null);
//        GameEventSystem.Instance.SetItemSelect(item);
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
