using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject player;

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
    }
    public void LoadObjects(GameData data)
    {
        
        player.transform.position = data.playerPosition;
        enemy1.SetActive(data.enemy1Alive);
        enemy2.SetActive(data.enemy2Alive);
    }
}
