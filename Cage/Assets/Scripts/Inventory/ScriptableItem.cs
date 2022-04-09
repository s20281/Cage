using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item")]
public class ScriptableItem : ScriptableObject
{
    public string name;
    public string character;
    public string description;
    public Sprite icon;
    public Skill skill;
    public bool singleUse;
    public int count;
}
