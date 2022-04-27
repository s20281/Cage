using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipTurn : MonoBehaviour
{
    public void skip()
    {
        Turn.control.usedTurn();
    }
}
