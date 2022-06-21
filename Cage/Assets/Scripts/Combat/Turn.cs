using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TurnState { PLAYER, ENEMY }

public class Turn : MonoBehaviour
{
    public static Turn control;
    public TurnState turnState;
    private Stats playerStats;
    public int alivePlayersCount;
    public int aliveEnemiesCount;
    public int turnNumber = 1;
    GameObject[] players;
    public List<GameObject> alivePlayers = new List<GameObject>();
    public GameObject[] enemies;
    public List<GameObject> aliveEnemies = new List<GameObject>();
    public Queue<GameObject> queue = new Queue<GameObject>();
    private bool allDead = false;
    private bool playerDead = false;
    public GameObject skillsPanel;
    public bool playerUsedTurn = false;

    public GameObject queuePanel;
    public GameObject queueIcon;
    public GameObject skipButton;

    public Stats objectStats;

    void Start()
    {
        control = this;
        skipButton.SetActive(false);
        GameEventSystem.Instance.OnSkillUse += usedTurn;
        GameEventSystem.Instance.OnEnemyDies += enemyDies;
        GameEventSystem.Instance.OnPlayerDies += playerDies;
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject p in players)
        {
            alivePlayers.Add(p);
        }
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies)
        {
            aliveEnemies.Add(e);
        }

        alivePlayersCount = players.Length;
        aliveEnemiesCount = enemies.Length;

        turnNumber = 1;

        EnemySkill.Start();

        StartCoroutine(nextTurn());
    }
    void EnqueAll()
    {
        for (int i = 0; i < alivePlayersCount + aliveEnemiesCount ; i++)
        {
            int maxSpeed = int.MinValue;
            GameObject fastest = new GameObject();
            Destroy(fastest);

            foreach (GameObject e in aliveEnemies)
            {
                int speed = e.GetComponent<Stats>().speed;
                if (!e.GetComponent<Stats>().queued && speed > maxSpeed)
                {
                    maxSpeed = speed;
                    fastest = e;
                }
            }
            foreach (GameObject p in players)
            {
                if (p.GetComponent<Stats>().isDead)
                    continue;
                int speed = p.GetComponent<Stats>().speed;
                if (!p.GetComponent<Stats>().queued && speed > maxSpeed)
                {
                    maxSpeed = speed;
                    fastest = p;
                }
            }
            if(!fastest.GetComponent<Stats>().isDead)
            {
                queue.Enqueue(fastest);

                fastest.GetComponent<Stats>().queueIcon = GameObject.Instantiate(queueIcon, queuePanel.transform.GetChild(1).transform, false);

                fastest.GetComponent<Stats>().queueIcon.GetComponent<Image>().sprite = fastest.GetComponent<SpriteRenderer>().sprite;

                ;
                fastest.GetComponent<Stats>().queued = true;
                Debug.Log((i + 1) + " " + fastest.name);
            }
        }
    }

    void removeAllBuffs()
    {
        foreach(GameObject p in alivePlayers)
        {
            Stats pStats = p.GetComponent<Stats>();
            
            foreach(Buff buff in pStats.buffsList)
            {
                buff.remove(pStats);
            }
        }
    }

    void returnToMap()
    {
        string level = PlayerPrefs.GetString("level");
        Debug.Log(level);
        SceneManager.LoadScene(level);
    }


    IEnumerator nextTurn()
    {
        while(true)
        {
            if (aliveEnemiesCount == 0)
            {
                Debug.Log("All enemies are dead");
                returnToMap();
                yield break;
            }
            if (alivePlayersCount == 0)
            {
                Debug.Log("Player is dead");
                yield return new WaitForSeconds(10f);
                SceneManager.LoadScene("Game Over");
                yield break;
            }
            if (queue.Count == 0)
            {
                yield return new WaitForSeconds(0.5f);
                Debug.ClearDeveloperConsole();
                Debug.Log("TURN " + turnNumber);
                queuePanel.transform.GetChild(0).GetComponent<Text>().text = turnNumber.ToString() + "\nTURN";
                turnNumber++;
                EnqueAll();
                yield return new WaitForSeconds(1.5f);
            }

            GameObject o = queue.Dequeue();
            objectStats = o.GetComponent<Stats>();
            objectStats.queued = false;

            if (objectStats.isDead)
                continue;

            Debug.Log(o.name + "'s Turn");

            bool isStunned = false;
            bool isBleeding = false;
            bool isHealing = false;

            int effectsCount = objectStats.effectsList.Count;

            if (effectsCount > 0)
            {
                for(int i = 0; i < effectsCount; i++)
                {
                    Effect e = objectStats.effectsList[i];

                    if (e.damagePerTurn != 0)
                    {
                        objectStats.healthChange(-e.damagePerTurn);
                    }

                    if (e.name == EffectName.STUN)
                        isStunned = true;

                    if (e.name == EffectName.BLEEDING && e.turnsCount > 0)
                        isBleeding = true;

                    if (e.name == EffectName.HEAL && e.turnsCount > 0)
                        isHealing = true;

                    e.turnsCount--;
                }
            }

            if (objectStats.isDead)
            {
                o.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("DEAD", Color.red);
                Destroy(objectStats.queueIcon);
                o.transform.GetChild(0).GetComponent<SkillEffects>().switchOff();
                continue;
            }

            bool skipsTurn = false;

            if (isStunned)
            {
                o.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("STUNNED", Color.yellow);
                Destroy(objectStats.queueIcon);
                Debug.Log(o.name + " is stunned");
                skipsTurn = true;
            }

            isStunned = false;
            isBleeding = false;
            bool isShielded = false;
            bool isProtected = false;

            if (effectsCount > 0)
            {
                for (int i = 0; i < effectsCount; i++)
                {
                    Effect e = objectStats.effectsList[i];

                    if (e.turnsCount <= 0)
                    {
                        objectStats.effectsList.Remove(e);
                        effectsCount--;
                        continue;
                    }
                    if (e.name == EffectName.STUN)
                        isStunned = true;

                    if (e.name == EffectName.BLEEDING && e.turnsCount > 0)
                        isBleeding = true;

                    if (e.name == EffectName.HEAL && e.turnsCount > 0)
                        isHealing = true;

                    if (e.name == EffectName.SHIELD && e.turnsCount > 0)
                        isShielded = true;

                    if (e.name == EffectName.PROTECTION && e.turnsCount > 0)
                        isProtected = true;


                }
            }

            if(!isStunned)
                o.transform.GetChild(0).GetComponent<SkillEffects>().setStunIcon(false);
            if (!isBleeding)
                o.transform.GetChild(0).GetComponent<SkillEffects>().setBleedingIcon(false);
            if (!isHealing)
                o.transform.GetChild(0).GetComponent<SkillEffects>().setHealIcon(false);
            if (!isShielded)
                objectStats.setShield(false);
            if (!isProtected && objectStats.isProtected)
            {
                objectStats.isProtected = false;
                o.transform.GetChild(0).GetComponent<SkillEffects>().setProtectionIcon(false);
            }
                objectStats.setShield(false);

            if (skipsTurn)
                continue;


            if (o.CompareTag("Enemy"))
            {
                turnState = TurnState.ENEMY;
                
                yield return new WaitForSeconds(1.0f);
                Color c = o.GetComponent<SpriteRenderer>().color;
                float red = c.r;
                float green = c.g;
                float blue = c.b;
                o.GetComponent<SpriteRenderer>().color = new Color(red + 20, green + 20, blue + 20);
                yield return new WaitForSeconds(1.5f);

                EnemySkill.UseSkill(objectStats, getRandomPlayer().GetComponent<Stats>());
                Destroy(objectStats.GetComponent<Stats>().queueIcon);



                o.GetComponent<SpriteRenderer>().color = new Color(red, green, blue); ;
                yield return new WaitForSeconds(1f);

            }
            else
            {
                turnState = TurnState.PLAYER;
                playerStats = o.GetComponent<Stats>();
                
                //skillsPanel.SetActive(true);
                playerUsedTurn = false;
                skipButton.SetActive(true);
                o.transform.GetChild(2).transform.gameObject.SetActive(true);
               

                while (!playerUsedTurn)
                {
                    yield return null;
                }
                o.transform.GetChild(2).transform.gameObject.SetActive(false);
                skipButton.SetActive(false);
                yield return new WaitForSeconds(1.0f);
                //skillsPanel.SetActive(false);
                //o.transform.GetChild(2).transform.gameObject.SetActive(false);
                o.GetComponent<InventoryForCombat>().ReloadItems();
            }

            // Buffy

            int buffsCount = objectStats.buffsList.Count;

            for (int i = 0; i < buffsCount ; i++) // tu jest jakiœ problem !
            {
                Buff buff = objectStats.buffsList[i];

                buff.turns--;

                if (buff.turns <= 0)
                {
                    buff.remove(objectStats);
                    buffsCount--;
                }
                    
            }
            Destroy(objectStats.queueIcon);
        }   
    }

    public void enemyAttack(GameObject enemy)
    {
        Stats enemyStats = enemy.GetComponent<Stats>();
        GameObject player = getRandomPlayer();
        playerStats = player.GetComponent<Stats>();

        float modificator = enemyStats.aim - playerStats.dodge;
        float roll = Random.Range(-5, 5);
        

        if (roll + modificator < 0)
        {
            Debug.Log(enemy.name + " missed");
            
            enemy.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("MISS", Color.yellow);
            player.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect("DODGE", Color.green);
            Destroy(enemy.GetComponent<Stats>().queueIcon);
            return;
        }

        int damage = enemyStats.strength;
        playerStats.healthChange(-damage);
        player.transform.GetChild(1).transform.GetChild(0).GetComponent<Effects>().displayEffect(damage.ToString(), Color.red);
        Debug.Log(enemy.name + " attacks " + player.name + " for " + damage);

        
    }

    public void usedTurn()
    {
        playerUsedTurn = true;
    }

    void playerDies(GameObject player)
    {
        alivePlayersCount--;
        alivePlayers.Remove(player);
    }

    void enemyDies()
    {
        aliveEnemiesCount--;
    }

    public void AutoWin()
    {
        Debug.Log("Auto WIN");
        returnToMap();
    }

    public GameObject getRandomPlayer()
    {
        int index = Random.Range(0, alivePlayers.Count);
        return alivePlayers[index];
    }

    public Stats getActivePlayer()
    {
        return playerStats;
    }

    public List<Stats> getAliveEnemies()
    {
        List<Stats> result = new List<Stats>();

        foreach(GameObject enemy in aliveEnemies)
        {
            Stats s = enemy.GetComponent<Stats>();
            if(!s.isDead)
                result.Add(s);
        }
        return result;
    }

    public void updateEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        aliveEnemiesCount = enemies.Length;
    }
}