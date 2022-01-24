using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { PLAYER, ENEMY }

public class Turn : MonoBehaviour
{
    public TurnState turnState;
    public Stats playerStats;
    public int alivePlayers;
    public int aliveEnemies;
    int turnNumber = 1;
    GameObject[] players;
    GameObject[] enemies;
    Queue<GameObject> queue = new Queue<GameObject>();
    private bool allDead = false;
    private bool playerDead = false;
    public GameObject skillsPanel;
    public bool playerUsedTurn = false;

    void Start()
    {
        GameEventSystem.Instance.OnSkillUse += usedTurn;
        GameEventSystem.Instance.OnEnemyDies += enemyDies;
        GameEventSystem.Instance.OnPlayerDies += playerDies;
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        alivePlayers = players.Length;
        aliveEnemies = enemies.Length;

        //if (alivePlayers > 0)
        //    Debug.Log("Wygrana");
        //else
        //    Debug.Log("Przegrana");

        turnNumber = 1;
        StartCoroutine(nextTurn());
    }
    void EnqueAll()
    {
        for (int i = 0; i < enemies.Length + players.Length; i++)
        {
            int maxSpeed = int.MinValue;
            GameObject fastest = new GameObject();
            Destroy(fastest);

            foreach (GameObject e in enemies)
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
                fastest.GetComponent<Stats>().queued = true;
                Debug.Log((i + 1) + " " + fastest.name);
            }
        }
    }


    IEnumerator nextTurn()
    {
        while(true)
        {
            if (aliveEnemies == 0)
            {
                Debug.Log("All enemies are dead");
                yield break;
            }
            if (alivePlayers == 0)
            {
                Debug.Log("Player is dead");
                yield break;
            }
            if (queue.Count == 0)
            {
                yield return new WaitForSeconds(0.5f);
                Debug.ClearDeveloperConsole();
                Debug.Log("TURN " + turnNumber);
                turnNumber++;
                EnqueAll();
                yield return new WaitForSeconds(1.5f);
            }

            GameObject o = queue.Dequeue();
            o.GetComponent<Stats>().queued = false;

            Debug.Log(o.name + "'s Turn");

            if (o.CompareTag("Enemy"))
            {
                turnState = TurnState.ENEMY;

                if (!o.GetComponent<Stats>().isDead)
                {
                    yield return new WaitForSeconds(2.0f);
                    enemyAttack(o);
                }
            }
            else
            {
                turnState = TurnState.PLAYER;
                if (!o.GetComponent<Stats>().isDead)
                {
                    skillsPanel.SetActive(true);
                    playerUsedTurn = false;

                    while (!playerUsedTurn)
                    {
                        yield return null;
                    }
                    skillsPanel.SetActive(false);
                }
            }
        }   
    }

    public void enemyAttack(GameObject enemy)
    {
        Stats enemyStats = enemy.GetComponent<Stats>();

        float modificator = enemyStats.aim - playerStats.dodge;
        float roll = Random.Range(-5, 5);

        if (roll + modificator < 0)
        {
            Debug.Log(enemy.name + " missed");
            return;
        }

        int damage = enemyStats.strength;
        playerStats.onHealthChange(-damage);
        Debug.Log(enemy.name + " attacks for " + damage);
    }

    void usedTurn()
    {
        playerUsedTurn = true;
    }

    void playerDies()
    {
        alivePlayers--;
    }

    void enemyDies()
    {
        aliveEnemies--;
    }

    public void AutoWin()
    {
        Debug.Log("Auto WIN");
        SceneManager.LoadScene("Level 1");
    }
}