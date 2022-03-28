using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    private Image spriteImage;
    private UIItem selectedItem;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();

    }

    public void UpdateItem(Item item)
    {

        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            Debug.Log(this.item.icon.name);
            spriteImage.sprite = this.item.icon;

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
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            }
            else
            {
                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }

        }
        else if (selectedItem.item != null)
        {
            if (SceneManager.GetActiveScene().name == "Combat")
            {
                GameEventSystem.Instance.SetItemSelect(Skill.NONE);
            }

            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }
}
