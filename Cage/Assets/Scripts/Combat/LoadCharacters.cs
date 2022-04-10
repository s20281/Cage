using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroName
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

    //public GameObject playerPrefab;
    //public GameObject hulkPrefab;
    //public GameObject ninjaPrefab;

    public GameObject heroPrefab;

    private bool hasCompanion;

    //private Dictionary<Hero, GameObject> heroMapping = new Dictionary<Hero, GameObject>();
    private Dictionary<string, HeroName> heroMapping = new Dictionary<string, HeroName>();


    Dictionary<int, GameObject> enemyPrefabs = new Dictionary<int, GameObject>();

    private void Awake()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GM");
        int[] enemiesToLoad = StaticClass.getEnemies();
        int enemiesCount = enemiesToLoad.Length;

        string[] heroesToLoad = new string[4];

        hasCompanion = Inventory.control.hasCompanion();

        if (hasCompanion)
        {
            Debug.Log("has companion");
            Character[] harr = Inventory.control.GetAllCharacters().ToArray();
            var allCharacters= Inventory.control.GetAllCharacters();
            Debug.Log("Loading");
            foreach (var name in harr)
            {
                Debug.Log(name.name);
            }

            for (int i = 0; i < 4; i++)
            {
                if (i >= harr.Length)
                    break;
                if (harr[i].name != "blank")
                {
                    heroesToLoad[i] = harr[i].name;
                    Debug.Log(harr[i].name);
                }
            }
        }
        else
            Debug.Log("no companion");


        heroMapping.Add("player", HeroName.PLAYER);
        heroMapping.Add("hulk", HeroName.HULK);
        heroMapping.Add("ninja", HeroName.NINJA);

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

        if (hasCompanion)
        {           
            GameObject[] heroSpawnPoints = new GameObject[] { heroSpawnPoint1, heroSpawnPoint2, heroSpawnPoint3, heroSpawnPoint4 };

            Hero [] heroes = gm.GetComponent<Team>().heroes;

            for (int i = 0; i < heroSpawnPoints.Length; i++)
            {
                if (heroes[i] != null)
                {
                    GameObject o = GameObject.Instantiate(heroPrefab, heroSpawnPoints[i].transform, false);
                    Hero h = heroes[i];
                    o.gameObject.GetComponent<Stats>().setStats(h);
                    o.name = h.name;
                    o.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
    }
}
