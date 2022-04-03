using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectName { STUN, BLEEDING}

public class Effect
{
    public EffectName name;
    public int turnsCount;
    public int damagePerTurn;
    public bool isStunned;

    public Effect(EffectName name, int turnsCount, int damagePerTurn, bool isStunned)
    {
        this.name = name;
        this.turnsCount = turnsCount;
        this.damagePerTurn = damagePerTurn;
        this.isStunned = isStunned;
    }
}
