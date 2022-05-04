using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Experience
{
    public static int getLevel(int exp)
    {
        if (exp < 10)
            return 1;
        else
            return 2;
    }
}
