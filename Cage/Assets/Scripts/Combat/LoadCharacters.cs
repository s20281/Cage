using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySkill;

public enum HeroName
{
    PLAYER, NINJA, HULK
}
public class LoadCharacters : MonoBehaviour
{
    public static LoadCharacters control;

    GameObject[] enemySpawnPoints;
    public GameObject enemySpawnPoint1;
    public GameObject enemySpawnPoint2;
    public GameObject enemySpawnPoint3;
    public GameObject enemySpawnPoint4;

    public GameObject heroSpawnPoint1;
    public GameObject heroSpawnPoint2;
    public GameObject heroSpawnPoint3;
    public GameObject heroSpawnPoint4;


    public GameObject none;
    public GameObject shadow;
    public GameObject rock;
    public GameObject spider;
    public GameObject zombie;
    public GameObject clown;
    public GameObject witch;
    public GameObject rat;
    public GameObject tornado;

    //public GameObject playerPrefab;
    //public GameObject hulkPrefab;
    //public GameObject ninjaPrefab;

    public GameObject heroPrefab;

    private bool hasCompanion;

    //private Dictionary<Hero, GameObject> heroMapping = new Dictionary<Hero, GameObject>();
    private Dictionary<string, HeroName> heroMapping = new Dictionary<string, HeroName>();


    Dictionary<EnemyType, GameObject> enemyPrefabs = new Dictionary<EnemyType, GameObject>();

    private void Awake()
    {
        control = this;

        GameObject gm = GameObject.FindGameObjectWithTag("GM");
        EnemyType[] enemiesToLoad = StaticClass.getEnemies();
        int enemiesCount = enemiesToLoad.Length;

        string[] heroesToLoad = new string[4];

        hasCompanion = Inventory.control.hasCompanion();

        if (hasCompanion)
        {
            Character[] harr = Inventory.control.GetAllCharacters().ToArray();
//            var allCharacters= Inventory.control.GetAllCharacters();           
            
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

        enemyPrefabs.Add(EnemyType.NONE, none);
        enemyPrefabs.Add(EnemyType.SHADOW, shadow);
        enemyPrefabs.Add(EnemyType.ROCK, rock);
        enemyPrefabs.Add(EnemyType.SPIDER, spider);
        enemyPrefabs.Add(EnemyType.ZOMBIE, zombie);
        enemyPrefabs.Add(EnemyType.CLOWN, clown);
        enemyPrefabs.Add(EnemyType.WITCH, witch);
        enemyPrefabs.Add(EnemyType.RAT, rat);
        enemyPrefabs.Add(EnemyType.TORNADO, tornado);

        enemySpawnPoints = new GameObject[] { enemySpawnPoint1, enemySpawnPoint2, enemySpawnPoint3, enemySpawnPoint4 };

        for(int i=0; i<4; i++)
        {
            if(enemiesToLoad[i] != EnemyType.NONE && enemyPrefabs.ContainsKey(enemiesToLoad[i]))
            {
                GameObject o = GameObject.Instantiate(enemyPrefabs[enemiesToLoad[i]], enemySpawnPoints[i].transform, false);
                o.name = enemyPrefabs[enemiesToLoad[i]].name;

                Vector3 spawnPosition = enemySpawnPoints[i].transform.position;
                Vector3 updatedSpawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + o.GetComponent<SpriteRenderer>().bounds.size.y / 2);
                o.transform.position = updatedSpawnPosition;
            }  
        }

        //if (hasCompanion)
        //{           
            GameObject[] heroSpawnPoints = new GameObject[] { heroSpawnPoint1, heroSpawnPoint2, heroSpawnPoint3, heroSpawnPoint4 };

            Hero [] heroes = gm.GetComponent<Team>().heroes;

            for (int i = 0; i < heroSpawnPoints.Length; i++)
            {
                if (heroes[i] != null)
                {
                    Hero h = heroes[i];
                    GameObject o = GameObject.Instantiate(h.combatPrefab, heroSpawnPoints[i].transform);
                    o.gameObject.GetComponent<Stats>().setStats(h);
                    o.name = h.name;
                    o.transform.GetChild(2).gameObject.SetActive(false);

                    Vector3 spawnPosition = heroSpawnPoints[i].transform.position;
                    Vector3 updatedSpawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + o.GetComponent<SpriteRenderer>().bounds.size.y / 2);
                    o.transform.position = updatedSpawnPosition;
                }
            }
        //}
    }

    public void summon(EnemyType type, int count)
    {
        int counter = 0;
        for (int i = 0; i < 4 && counter < count; i++)
        {
            if (enemySpawnPoints[i].transform.childCount > 0)
            {
                Stats enemy = enemySpawnPoints[i].transform.GetChild(0).gameObject.GetComponent<Stats>();

                if (!enemy.isDead)
                    continue;

                Turn.control.aliveEnemies.Remove(enemy.gameObject);
                Destroy(enemy.gameObject);
            }

            GameObject o = GameObject.Instantiate(enemyPrefabs[type], enemySpawnPoints[i].transform, false);
            o.name = enemyPrefabs[type].name;

            Vector3 spawnPosition = enemySpawnPoints[i].transform.position;
            Vector3 updatedSpawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + o.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            o.transform.position = updatedSpawnPosition;

            Turn.control.aliveEnemies.Add(o);
            Turn.control.aliveEnemiesCount++;
            //Turn.control.updateEnemies();

            counter++;
        }
    }
}
