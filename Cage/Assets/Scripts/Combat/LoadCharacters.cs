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


    Dictionary<int, GameObject> enemyPrefabs = new Dictionary<int, GameObject>();

    private void Awake()
    {
        int[] enemiesToLoad = StaticClass.getEnemies();
        int enemiesCount = enemiesToLoad.Length;

        enemyPrefabs.Add(0, enemy0Prefab);
        enemyPrefabs.Add(1, enemy1Prefab);

        GameObject[] enemySpawnPoints = new GameObject[] { enemySpawnPoint1, enemySpawnPoint2, enemySpawnPoint3, enemySpawnPoint4 };

        for(int i=0; i<4; i++)
        {
            if(enemiesToLoad[i] != 0 && enemyPrefabs.ContainsKey(enemiesToLoad[i]))
            {
                GameObject.Instantiate(enemyPrefabs[enemiesToLoad[i]], enemySpawnPoints[i].transform, false);
            }  
        }
    }
}
