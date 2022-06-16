using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public Hero playerWithStats;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    // Start is called before the first frame update
    void Start()
    {
        if (playerWithStats.mentalHealth <= 0)
        {
            text1.SetActive(true);

        }
        else
        {
            if(playerWithStats.mentalHealth >0 && playerWithStats.mentalHealth <= 100)
            {
                text2.SetActive(true);
            }
            else
            {
                text3.SetActive(true);
            }
        }
        
    }

    
}
