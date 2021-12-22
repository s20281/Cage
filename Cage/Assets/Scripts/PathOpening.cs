using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathOpening : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void OnTriggerStay2D(Collider2D other)
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("click");
            spriteRenderer.color = new Color(1, 1, 1, 0);
            spriteRenderer.GetComponent<Collider2D>().enabled = false;
        }

    }


    }
