using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    public static bool loadScene = false;

    public static int enemy1 = 0;
    public static int enemy2 = 0;
    public static int enemy3 = 0;
    public static int enemy4 = 0;

    public static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

    public static void setEnemies(int [] enemies)
    {
        enemy1 = enemies[0];
        enemy2 = enemies[1];
        enemy3 = enemies[2];
        enemy4 = enemies[3];
    }
    public static int[] getEnemies()
    {
        return new int[] { enemy1, enemy2, enemy3, enemy4 };
    }




}
