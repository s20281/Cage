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

    public void setHealIcon(bool active)
    {
        gameObject.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(active);
    }

    public void setShieldIcon(bool active)
    {
        gameObject.transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(active);
    }

    public void setProtectionIcon(bool active)
    {
        gameObject.transform.GetChild(2).transform.GetChild(4).gameObject.SetActive(active);
    }

    public void switchOff()
    {
        setBleedingIcon(false);
        setStunIcon(false);
        setHealIcon(false);
        setShieldIcon(false);
    }
}
