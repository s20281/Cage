using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response 
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject objectToDrop;


    public string ResponseText => responseText;

    public DialogueObject DialogueObject => dialogueObject;
    public GameObject GameObject => objectToDrop;
}
