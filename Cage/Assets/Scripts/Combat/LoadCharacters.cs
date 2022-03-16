using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacters : MonoBehaviour
{
    public GameObject enemySpawnPoint1;
    public GameObject enemySpawnPoint2;
    public GameObject enemySpawnPoint3;
    public GameObject enemySpawnPoint4;

    public GameObject enemy0Prefab;
    public GameObject enemy1Prefab;
    //public GameObject enemy2Prefab;

    int enemiesCount = 4;

    Dictionary<int, GameObject> enemyPrefabs = new Dictionary<int, GameObject>();

    int[] enemiesToLoad = new int[] { 1, 0, 1, 0 };

    void Start()
    {
        enemyPrefabs.Add(0, enemy0Prefab);
        enemyPrefabs.Add(1, enemy1Prefab);

        GameObject.Instantiate(enemyPrefabs[enemiesToLoad[0]], enemySpawnPoint1.transform, false);

        if(enemiesCount > 1)
            GameObject.Instantiate(enemyPrefabs[enemiesToLoad[1]], enemySpawnPoint2.transform, false);
        if (enemiesCount > 2)
            GameObject.Instantiate(enemyPrefabs[enemiesToLoad[2]], enemySpawnPoint3.transform, false);
        if (enemiesCount > 3)
            GameObject.Instantiate(enemyPrefabs[enemiesToLoad[3]], enemySpawnPoint4.transform, false);
    }

    void Update()
    {
        
    }
}
