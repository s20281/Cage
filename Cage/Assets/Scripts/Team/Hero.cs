using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero")]
public class Hero : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;
    public int maxHealth;
    public int health;
    public int speed;
    public int dodge;
    public int accuracy;
    public int strength;
}
