using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyName { Shadow, Rock, Spider, None}

public class EnemyInfo : MonoBehaviour
{
    public EnemyName[] enemiesToLoad = new EnemyName[4] { EnemyName.None, EnemyName.None, EnemyName.None, EnemyName.None };
}
