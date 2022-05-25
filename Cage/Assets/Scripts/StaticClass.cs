using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySkill;

public static class StaticClass
{
    public static bool loadScene = false;

    public static EnemyType enemy1 = EnemyType.NONE;
    public static EnemyType enemy2 = EnemyType.NONE;
    public static EnemyType enemy3 = EnemyType.NONE;
    public static EnemyType enemy4 = EnemyType.NONE;

    public static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

    public static void setEnemies(EnemyType[] enemies)
    {
        enemy1 = enemies[0];
        enemy2 = enemies[1];
        enemy3 = enemies[2];
        enemy4 = enemies[3];
    }
    public static EnemyType[] getEnemies()
    {
        return new EnemyType[] { enemy1, enemy2, enemy3, enemy4 };
    }




}
