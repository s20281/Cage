using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public Character character;
    private Image spriteImage;
    private UIItem selectedItem;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null, null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();

    }

    public void UpdateItem(Item item, Character character)
    {
        this.item = item;
        this.character = character;
      
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;

        }
        else if(this.character != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.character.icon;
        } 
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (this.item != null)
        {
            if (SceneManager.GetActiveScene().name == "Combat")
            {
                Debug.Log(item.skill);
                GameEventSystem.Instance.SetItemSelect(item.skill);
            }


            if (selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(this.item, null);
                UpdateItem(clone, null);
            }
            else
            {
                selectedItem.UpdateItem(this.item, null);
                UpdateItem(null, null);
            }

        }
        else if (this.character != null)
        {
           

                if (selectedItem.character != null)
                {

                    var slotIndex = TeamContainer.control.FindIndexOfCharacter(this.character);
                    Debug.Log(slotIndex);
                    Inventory.control.GetAllCharacters()[slotIndex] = selectedItem.character;

                    Character clone = new Character(selectedItem.character);
                    selectedItem.UpdateItem(null, this.character);
                    UpdateItem(null, clone);
                }
                else
                {

                if (this.character.name != "blank")
                {
                    var slotIndex = TeamContainer.control.FindIndexOfCharacter(this.character);
                    Debug.Log(slotIndex);
                    Inventory.control.GetAllCharacters()[slotIndex] = Inventory.control.FindCharacter(0);

                    selectedItem.UpdateItem(null, this.character);
                    UpdateItem(null, null);
                }



                }
            }
            else if (selectedItem.item != null)
            {
                if (SceneManager.GetActiveScene().name == "Combat")
                {
                    GameEventSystem.Instance.SetItemSelect(Skill.NONE);
                }

                UpdateItem(selectedItem.item, null);
                selectedItem.UpdateItem(null, null);

            }
            else if (selectedItem.character != null)
            {

                UpdateItem(null, selectedItem.character);


                var slotIndex = TeamContainer.control.FindIndexOfCharacter(selectedItem.character);
                Debug.Log("exchange: " + slotIndex);
                Inventory.control.GetAllCharacters()[slotIndex] = selectedItem.character;

                selectedItem.UpdateItem(null, null);


            }

        
    }
}
