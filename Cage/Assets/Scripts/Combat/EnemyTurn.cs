using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : MonoBehaviour
{

    public Stats playerStats;
    GameObject[] enemies;
    Queue<GameObject> enemiesQueue = new Queue<GameObject>();
    private bool allDead = false;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            int maxSpeed = int.MinValue;
            GameObject fastestEnemy = new GameObject();
            Destroy(fastestEnemy);

            foreach (GameObject e in enemies)
            {
                int speed = e.GetComponent<Stats>().speed;
                if (!e.GetComponent<Stats>().queued && speed > maxSpeed)
                {
                    maxSpeed = speed;
                    fastestEnemy = e;
                }
            }
            enemiesQueue.Enqueue(fastestEnemy);
            fastestEnemy.GetComponent<Stats>().queued = true;
            Debug.Log((i + 1) + " " + fastestEnemy.name);
        }
    }

    void Update()
    {

    }

    public void Atack()
    {
        GameObject enemy = nextEnemy();
        if (allDead)
        {
            Debug.Log("All enemies are dead");
            return;
        }


        Stats enemyStats = enemy.GetComponent<Stats>();

        float modificator = enemyStats.aim - playerStats.dodge;
        float roll = Random.Range(-5, 5);

        if (roll + modificator < 0)
        {
            Debug.Log(enemy.name + " missed");
            return;
        }

        int damage = 3 + enemyStats.strength;
        playerStats.onHealthChange(-damage);
        Debug.Log(enemy.name + " attacks for " + damage);
    }

    GameObject nextEnemy()
    {
        GameObject enemy = enemiesQueue.Dequeue();

        if (enemy.GetComponent<Stats>().isDead)
        {
            if (enemiesQueue.Count == 0)
            {
                allDead = true;
                return new GameObject();
            }
            return nextEnemy();
        }
        enemiesQueue.Enqueue(enemy);
        return enemy;
    }
}