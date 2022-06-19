using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableItemMap : MonoBehaviour
{
    public ScriptableItem sword;
    public ScriptableItem baseball;
    public ScriptableItem flashbang;
    public ScriptableItem hammer;
    public ScriptableItem katana;
    public ScriptableItem key;
    public ScriptableItem potion;
    public ScriptableItem shield;
    public ScriptableItem shuriken;
    public Dictionary<string, ScriptableItem> mapping;


    private void Start()
    {
        mapping = new Dictionary<string, ScriptableItem>()
        {
            {sword.name, sword },
            {baseball.name, baseball },
            {flashbang.name, flashbang },
            {hammer.name, hammer },
            {katana.name, katana },
            {key.name, key },
            {potion.name, potion },
            {shield.name, shield },
            {shuriken.name, shuriken }
        };
    }
}
