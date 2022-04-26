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
    private List<ScriptableItem> items;
    public static InventoryForCombat control;
    GameObject inventoryCanvas;
    private GameObject gm;

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
        //inventory 1.0
        //inv = Inventory.control;
        //Item item = inv.FindItem(1);
        //itemsInInventory = inv.GetAllItems();

        //foreach (var invItem in itemsInInventory)
        //{
        //    if (invItem.character == this.gameObject.name)
        //    {
        //        Debug.Log(invItem.name);
        //        AddNewItem(invItem);
        //    }
        //}

        //inventory 2.0
        LoadItems();
        //GameEventSystem.Instance.OnSkillUse += reloadItems;
    }

    private void LoadItems()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        items = gm.GetComponent<Inventory2>().items;

        foreach (var i in items)
        {
            if (i.character == this.gameObject.name)
            {
                Debug.Log("ITEM: " + i.name);
                AddNewItem(new Item(i));
            }
        }
        UseSkill.control.actSkill = Skill.NONE;
    }

    public void ReloadItems()
    {
        UIItem.control.selectedItem.UpdateItem(null, null);

        var count = 0;
        foreach (var i in items)
        {
            if (i.character == this.gameObject.name)
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
        GameEventSystem.Instance.SetItemSelect(item);
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
