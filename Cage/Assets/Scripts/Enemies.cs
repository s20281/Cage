using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySkill;

public class Enemies
{
    static Dictionary<string, int> shadowStats = new Dictionary<string, int>
    {
        {"maxHealth", 5 },
        {"speed", 7 },
        {"strength", 2},
        {"dodge", 6 },
        {"aim", 6 }
    };

    static Dictionary<string, int> rockStats = new Dictionary<string, int>
    {
        {"maxHealth", 20 },
        {"speed", 2 },
        {"strength", 6},
        {"dodge", 1 },
        {"aim", 3 }
    };

    static Dictionary<string, int> spiderStats = new Dictionary<string, int>
    {
        {"maxHealth", 10 },
        {"speed", 5 },
        {"strength", 5},
        {"dodge", 5 },
        {"aim", 5 }
    };

    static Dictionary<string, int> witchStats = new Dictionary<string, int>
    {
        {"maxHealth", 10 },
        {"speed", 8 },
        {"strength", 2},
        {"dodge", 3 },
        {"aim", 7 }
    };

    static Dictionary<string, int> ratStats = new Dictionary<string, int>
    {
        {"maxHealth", 3 },
        {"speed", 10 },
        {"strength", 2},
        {"dodge", 3 },
        {"aim", 5 }
    };

    static Dictionary<string, int> zombieStats = new Dictionary<string, int>
    {
        {"maxHealth", 15 },
        {"speed", 1 },
        {"strength", 3},
        {"dodge", 0 },
        {"aim", 3 }
    };

    static Dictionary<string, int> tornadoStats = new Dictionary<string, int>
    {
        {"maxHealth", 3 },
        {"speed", 10 },
        {"strength", 3},
        {"dodge", 30 },
        {"aim", 7 }
    };

    static Dictionary<string, int> clownStats = new Dictionary<string, int>
    {
        {"maxHealth", 20 },
        {"speed", 10 },
        {"strength", 3},
        {"dodge", 6 },
        {"aim", 4 }
    };

    static Dictionary<string, int> clownCloneStats = new Dictionary<string, int>
    {
        {"maxHealth", 1 },
        {"speed", 10 },
        {"strength", 3},
        {"dodge", 6 },
        {"aim", 4 }
    };



    public static Dictionary<EnemyType, Dictionary<string, int>> enemyStat = new Dictionary<EnemyType, Dictionary<string, int>>{

        {EnemyType.SHADOW, shadowStats },
        {EnemyType.ROCK, rockStats },
        {EnemyType.CLOWN, clownStats},
        {EnemyType.TORNADO, tornadoStats},
        {EnemyType.SPIDER, spiderStats},
        {EnemyType.WITCH, witchStats},
        {EnemyType.ZOMBIE, zombieStats},
        {EnemyType.RAT, ratStats },
        {EnemyType.CLOWN_CLONE, clownCloneStats}
    };
}
