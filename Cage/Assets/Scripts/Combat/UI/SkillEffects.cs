using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffects : MonoBehaviour
{
    public void setBleedingIcon(bool active)
    {
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(active);
    }

    public void setStunIcon(bool active)
    {
        gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(active);
    }

    public void switchOff()
    {
        setBleedingIcon(false);
        setStunIcon(false);
    }
}
