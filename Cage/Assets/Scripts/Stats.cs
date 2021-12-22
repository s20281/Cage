using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int health;
    public int speed;
    public int strength;
    public int dodge;
    public int aim;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnMouseOver()
    {
        Debug.Log(gameObject.name);
        GameEventSystem.Instance.SetMouseHoverOnEnemy(this);
    }
}
