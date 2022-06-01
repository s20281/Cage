using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public void attack()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("attack");
    }

    public void hit()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("hit");
    }
}
