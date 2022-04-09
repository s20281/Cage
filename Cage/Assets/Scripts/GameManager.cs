using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            DestroyImmediate(gameObject);
        else if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
