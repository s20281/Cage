using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WalkPlayer"))
        {
            collision.transform.position = Destination.transform.position;
        }
    }
}
