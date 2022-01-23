using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    private static GameEventSystem instance;
    
    public static GameEventSystem Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameEventSystem>();
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public event Action<Stats> OnMouseOverEnemy;
    public event Action<Stats> OnMouseExitEnemy;
    public event Action<Stats> OnPositiveSkillUse;
    public event Action OnSkillUse;
    public event Action<bool> OnPlayerStatsActive;
    public event Action<bool> OnEnemyStatsActive;

    public void SetMouseOverEnemy(Stats stats)
    {
        OnMouseOverEnemy.Invoke(stats);
    }

    public void SetMouseExitEnemy(Stats stats)
    {
        OnMouseExitEnemy.Invoke(stats);
    }
    public void SetPositiveSkillUse(Stats stats)
    {
        OnPositiveSkillUse.Invoke(stats);
    }

    public void SetSkillUse()
    {
        OnSkillUse.Invoke();
    }

    public void SetPlayerStatsActive(bool isActive)
    {
        OnPlayerStatsActive.Invoke(isActive);
    }

    public void SetEnemyStatsActive(bool isActive)
    {
        OnEnemyStatsActive.Invoke(isActive);
    }
}
