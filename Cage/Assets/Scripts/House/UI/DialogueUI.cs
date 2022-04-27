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
    private GameObject objectToAddToTeam;
    private string goodAnswer; 
    bool canBeRecruited;
    GameObject gm;

    void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        gm = GameObject.FindGameObjectWithTag("GM");
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

        if(dialogueObject.name == goodAnswer)
        {
            if (objectToDrop != null)
            {
                player.GetComponent<Inventory>().AddItem(objectToDrop.name);
                gm.gameObject.GetComponent<Inventory2>().items.Add(objectToDrop.GetComponent<PickableItem>().item);
            }

        }

        if (dialogueObject.name == "NextTime")
        {
            CloseDialogueBox();
        }
        else
        {

            if (dialogueObject.HasResponses)
            {
                responseHandler.ShowResponses(dialogueObject.Responses);
            }
            else
            {
                CloseDialogueBox();

                if (canBeRecruited)
                {
                    objectToAddToTeam.GetComponent<PickableItem>().PickItem(player.GetComponent<Inventory>());

                }
                else
                {
                    objectToAddToTeam.SetActive(false);
                    objectToAddToTeam.GetComponent<ObjectsManager>().setOff();
                }


            }
        }
       
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void GetObject(GameObject objectToDrop, string goodAnswer, bool canBeRecruited, GameObject objectToAddToTeam)
    {

        this.objectToDrop = objectToDrop;
        this.goodAnswer = goodAnswer;
        this.canBeRecruited = canBeRecruited;
        this.objectToAddToTeam = objectToAddToTeam;
    }
}
