using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero")]
public class Hero : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public string whatWantInTeammates; //docelowo mo¿e enum
    public Sprite icon;
    public int maxHealth;
    public int health;
    public int speed;
    public int dodge;
    public int accuracy;
    public int strength;
    public int mentalHealth;

    public int exp;
    public int level;
    public int levelUpPoints;

    public static Dictionary<int, int> expRequiredForLvl = new Dictionary<int, int>() { { 1, 0 }, { 2, 10 }, { 3, 15 }, { 4, 20 }, { 5, 25 }, { 6, 30 }, { 7, 35 }, { 8, 40 }, { 9, 45 }, { 10, 50 }, { 11, int.MaxValue} };


    public void addExp(int exp)
    {
        this.exp += exp;

        if(this.exp >= expRequiredForLvl[level+1])
        {
            level++;
            this.exp -= expRequiredForLvl[level];
            levelUpPoints++;
        }
    }
    // TODO zrobiæ ¿eby exp siêd dodawa³ te¿ jak ktoœ zginie od efektu
}


