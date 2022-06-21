using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamContainer : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfslots;
    private List<Character> charactersInInventory;
    public static TeamContainer control;

    private void Awake()
    {
        control = this;

        Debug.Log("Do it");
        for (int i = 0; i < numberOfslots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
        }

        getUIItemSize();


    }
   

    public void UpdateSlot(int slot, Character character)
    {
        uIItems[slot].UpdateItem(null,character);
      
    }
    public void AddNewCharacter(Character character)
    {

        UpdateSlot(uIItems.FindIndex(i => i.character == null), character);
    }

    public void RemoveCharacter(Character character)
    {
        UpdateSlot(uIItems.FindIndex(i => i.character == character), null);
    }

    public int FindIndexOfCharacter(Character character)
    {
       return uIItems.FindIndex(i => i.character == character);     
    }

    public int getUIItemSize()
    {
        Debug.Log("containerSize: " +uIItems.Count);
        return uIItems.Count;
    }
}
