using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject player;
    public GameObject doorLeft;
    public GameObject doorUp;
    public GameObject doorRight;

    public GameObject sword;
    public GameObject book;
    public GameObject potion;
    public GameObject key;

    void Start()
    {
        GameEventSystemMap.Instance.OnSaveData += SaveObjects;
        GameEventSystemMap.Instance.OnLoadData += LoadObjects;
    }

    public void SaveObjects(GameData data)
    {
        data.enemy1Alive = enemy1.activeSelf;
        data.enemy2Alive = enemy2.activeSelf;
        data.playerPosition = player.transform.position;
        data.doorLeftOpened = doorLeft.GetComponent<BoxCollider2D>().isActiveAndEnabled;
        data.doorRightOpened = doorRight.GetComponent<BoxCollider2D>().isActiveAndEnabled;
        data.doorUpOpened = doorUp.GetComponent<BoxCollider2D>().isActiveAndEnabled;

        data.swordPicked = sword.activeSelf;
        data.keyPicked = key.activeSelf;
        data.potionPicked = potion.activeSelf;
        data.bookPicked = book.activeSelf;

        
      
    }
    public void LoadObjects(GameData data)
    {
        
        player.transform.position = data.playerPosition;
        enemy1.SetActive(data.enemy1Alive);
        enemy2.SetActive(data.enemy2Alive);
        doorUp.SetActive(data.doorUpOpened);
        doorLeft.SetActive(data.doorLeftOpened);
        doorRight.SetActive(data.doorRightOpened);

        sword.SetActive(data.swordPicked);
        potion.SetActive(data.potionPicked);
        book.SetActive(data.bookPicked);

        if (!data.swordPicked)
        {
            player.GetComponent<Inventory>().AddItem("sword");
        }

        if (!data.potionPicked)
        {
            Debug.Log("AAA");
            player.GetComponent<Inventory>().AddItem("potion");
        }

        if (data.keyPicked)
        {
            player.GetComponent<Inventory>().AddItem("key");
        }
        // trzeba zrobiæ ¿eby siê dodawa³y do inventory
    }
}
