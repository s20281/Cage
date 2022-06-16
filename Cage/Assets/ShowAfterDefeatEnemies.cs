using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAfterDefeatEnemies : MonoBehaviour
{
    public GameObject itemToActivate;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    void Start()
    {
       
    }

    void Update()
    {
        if(enemy1 == null && enemy2 == null && enemy3 == null && enemy4 == null)
        {
            itemToActivate.active = true;

        }
        
    }
}
