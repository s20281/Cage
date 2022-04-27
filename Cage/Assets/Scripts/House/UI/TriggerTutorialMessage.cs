using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorialMessage : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "WalkPlayer")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "WalkPlayer")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
