using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WalkPlayer"))
        {
            print("Dupa");
            Color normalColor = gameObject.GetComponent<SpriteRenderer>().color;
            Color transColor = new Color(normalColor.r, normalColor.g, normalColor.g, 50);
            gameObject.GetComponent<SpriteRenderer>().color = transColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WalkPlayer"))
        {
            Color transColor = gameObject.GetComponent<SpriteRenderer>().color;
            Color normalColor = new Color(transColor.r, transColor.g, transColor.g, 100);
            gameObject.GetComponent<SpriteRenderer>().color = transColor;
        }
    }
}
