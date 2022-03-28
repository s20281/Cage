using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableCharacter : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogUI;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject objectToDrop;
    [SerializeField] private string goodAnswer;
    [SerializeField] private bool canBeRecruited;
    public void starTalking()
    {
        dialogUI.GetObject(objectToDrop, goodAnswer, canBeRecruited, gameObject);
        dialogUI.ShowDialogue(testDialogue);

    }
}
