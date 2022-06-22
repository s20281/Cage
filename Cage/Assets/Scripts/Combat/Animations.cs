using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public void attack()
    {
        if (gameObject.name == "Main")
            this.gameObject.GetComponent<Animator>().SetTrigger("attack");
    }

    public void hit()
    {
        if(gameObject.name == "Main")
            this.gameObject.GetComponent<Animator>().SetTrigger("hit");
    }
}
