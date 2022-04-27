using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    public static bool loadScene = false;

    public static EnemyName enemy1 = EnemyName.None;
    public static EnemyName enemy2 = EnemyName.None;
    public static EnemyName enemy3 = EnemyName.None;
    public static EnemyName enemy4 = EnemyName.None;

    public static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

    public static void setEnemies(EnemyName [] enemies)
    {
        enemy1 = enemies[0];
        enemy2 = enemies[1];
        enemy3 = enemies[2];
        enemy4 = enemies[3];
    }
    public static EnemyName[] getEnemies()
    {
        return new EnemyName[] { enemy1, enemy2, enemy3, enemy4 };
    }




}
