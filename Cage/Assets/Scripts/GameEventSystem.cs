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

    public event Action<Stats> OnMouseHoverOnEnemy;

    public void SetMouseHoverOnEnemy(Stats stats)
    {
        OnMouseHoverOnEnemy.Invoke(stats);
    }
}
