using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDatabase : MonoBehaviour
{
    public List<Character> characters = new List<Character>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Character GetCharacter(int id)
    {
        return characters.Find(character => character.id == id);
    }

    public Character GetCharacter(string name)
    {
        return characters.Find(character => character.name == name);
    }

    void BuildDatabase()
    {
        characters = new List<Character>()
        {
            new Character(0, "blank", ".", new Dictionary<string, int>
            {
                { "stat1", 0 },
                { "stat2", 0 }
            }),
            new Character(1, "player", "Uhahah.", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }),

            new Character(2, "hulk", "I'm going to overpower you.", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }),

            new Character(3, "ninja", "Hyaaa, ha, hya", new Dictionary<string, int>
            {
                { "stat1", 15 },
                { "stat2", 3 }
            }),

            new Character(4, "swordman","Fight! Fight!", new Dictionary<string, int>
            {
                { "stat1", 10 },
                { "stat2", 1 }
            }),


        };
    }
}
