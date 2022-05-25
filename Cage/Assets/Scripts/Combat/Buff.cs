using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    int speed;
    int strength;
    int dodge;
    int aim;
    public int turns;

    public Buff(int speed, int strength, int dodge, int aim, int turns)
    {
        this.speed = speed;
        this.strength = strength;
        this.dodge = dodge;
        this.aim = aim;
        this.turns = turns;
    }

    public void apply(Stats target)
    {
        target.speed += speed;
        target.strength += strength;
        target.dodge += dodge;
        target.aim += aim;
    }

    public void remove(Stats target)
    {
        target.buffsList.Remove(this);
        target.speed -= speed;
        target.strength -= strength;
        target.dodge -= dodge;
        target.aim -= aim;
    }
}
