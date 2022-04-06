using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hero
{
    PLAYER, NINJA, HULK
}

public class LoadCharacters : MonoBehaviour
{
    public GameObject enemySpawnPoint1;
    public GameObject enemySpawnPoint2;
    public GameObject enemySpawnPoint3;
    public GameObject enemySpawnPoint4;

    public GameObject heroSpawnPoint1;
    public GameObject heroSpawnPoint2;
    public GameObject heroSpawnPoint3;
    public GameObject heroSpawnPoint4;


    public GameObject enemy0Prefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;

    public GameObject playerPrefab;
    public GameObject hulkPrefab;
    public GameObject ninjaPrefab;

    private bool hasCompanion;

    //private Dictionary<Hero, GameObject> heroMapping = new Dictionary<Hero, GameObject>();
    private Dictionary<string, GameObject> heroMapping = new Dictionary<string, GameObject>();


    Dictionary<int, GameObject> enemyPrefabs = new Dictionary<int, GameObject>();

    private void Awake()
    {
        int[] enemiesToLoad = StaticClass.getEnemies();
        int enemiesCount = enemiesToLoad.Length;

        string[] heroesToLoad = new string[4];

        hasCompanion = Inventory.control.hasCompanion();

        if (hasCompanion)
        {
            Item[] harr = Inventory.control.GetAllCharacters().ToArray();
            for (int i = 0; i < 4; i++)
            {
                if (i >= harr.Length)
                    break;
                heroesToLoad[i] = harr[i].name;
                Debug.Log(harr[i].name);
            }
        }
       


        

        

        //heroMapping.Add(Hero.PLAYER, playerPrefab);
        //heroMapping.Add(Hero.HULK, hulkPrefab);
        //heroMapping.Add(Hero.NINJA, ninjaPrefab);

        heroMapping.Add("player", playerPrefab);
        heroMapping.Add("hulk", hulkPrefab);
        heroMapping.Add("ninja", ninjaPrefab);




        enemyPrefabs.Add(0, enemy0Prefab);
        enemyPrefabs.Add(1, enemy1Prefab);
        enemyPrefabs.Add(2, enemy2Prefab);

        GameObject[] enemySpawnPoints = new GameObject[] { enemySpawnPoint1, enemySpawnPoint2, enemySpawnPoint3, enemySpawnPoint4 };

        for(int i=0; i<4; i++)
        {
            if(enemiesToLoad[i] != 0 && enemyPrefabs.ContainsKey(enemiesToLoad[i]))
            {
                GameObject o = GameObject.Instantiate(enemyPrefabs[enemiesToLoad[i]], enemySpawnPoints[i].transform, false);
                o.name = enemyPrefabs[enemiesToLoad[i]].name;
            }  
        }

        //GameObject.Instantiate(heroMapping[Hero.PLAYER], heroSpawnPoint1.transform, false);
        GameObject p = GameObject.Instantiate(heroMapping["player"], heroSpawnPoint1.transform, false);
        p.name = heroMapping["player"].name;
        p.transform.GetChild(2).gameObject.SetActive(false);

        if (hasCompanion)
        {
            GameObject[] heroSpawnPoints = new GameObject[] { heroSpawnPoint2, heroSpawnPoint3, heroSpawnPoint4 };

            for (int i = 0; i < 3; i++)
            {
                if (heroesToLoad[i] != null && heroMapping.ContainsKey(heroesToLoad[i]))
                {
                    GameObject o = GameObject.Instantiate(heroMapping[heroesToLoad[i]], heroSpawnPoints[i].transform, false);
                    o.name = heroMapping[heroesToLoad[i]].name;
                    //o.transform.GetChild(2).gameObject.SetActive(false);  // co to mia³o robiæ?
                }
            }
        }
    }
}
