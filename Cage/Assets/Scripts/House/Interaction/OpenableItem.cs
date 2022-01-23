using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableItem : MonoBehaviour
{
    public void OpenIt()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;


    }
}
