using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableCharacter : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogUI;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject objectToDrop;
    [SerializeField] private string goodAnswer;
    public void starTalking()
    {
        dialogUI.GetObject(objectToDrop, goodAnswer);
        dialogUI.ShowDialogue(testDialogue);
    }
}
