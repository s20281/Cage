using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject player;

    private ResponseHandler responseHandler;
    private TypeWriterEffect typeWriterEffect;
    private GameObject objectToDrop;
    private string goodAnswer;

    // Start is called before the first frame update
    void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        //GetComponent<TypeWriterEffect>().Run("Pierwsza wiadomosc oby dzialalo,", textLabel);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {       
        dialogueBox.SetActive(true);       
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for(int i = 0; i<dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typeWriterEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.Responses != null && dialogueObject.Responses.Length > 0) break;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        
        }
        Debug.Log(dialogueObject.name);

        if(dialogueObject.name == goodAnswer)
        {
            //objectToDrop.SetActive(true);
            player.GetComponent<Inventory>().AddItem(objectToDrop.name);

        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else{
            CloseDialogueBox();
        }
       
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void GetObject(GameObject objectToDrop, string goodAnswer)
    {

        this.objectToDrop = objectToDrop;
        this.goodAnswer = goodAnswer;
    }
}
