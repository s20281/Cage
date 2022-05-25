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
    private GameObject objectWanted;
    private GameObject objectToAddToTeam;
    private string answerToGiveTheObject;
    private string goodAnswer;
    private bool haveToGiveObject;
    private bool canBeRecruited;
    private bool shouldOpenTheDoor;
    private GameObject doorToOpen;
    private GameObject gm;

    private bool ifHaveImpactOnMind;
    private int pointsToImpactTheMind;
    private Hero prefabToFindStats;

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

        if (haveToGiveObject)
        {
            if (dialogueObject.name == answerToGiveTheObject)
            {
                if (objectWanted != null)
                {
                    player.GetComponent<Inventory>().RemoveItem(objectWanted.name);
                    gm.gameObject.GetComponent<Inventory2>().items.Remove(objectWanted.GetComponent<PickableItem>().item);
                }

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

                Debug.Log(canBeRecruited);
                Debug.Log(dialogueObject.name);
                Debug.Log(goodAnswer);

                if (dialogueObject.name.ToLower() == goodAnswer.ToLower())
                {                 

                    if (shouldOpenTheDoor = true && doorToOpen != null)
                    {
                        if (doorToOpen.GetComponent<OpenableItem>() != null)
                        {
                            Debug.Log("Open");
                            doorToOpen.GetComponent<OpenableItem>().doorOpening();
                        }
                    }
                }

                if (canBeRecruited && dialogueObject.name.ToLower() == goodAnswer.ToLower())
                {
                    Debug.Log("pick");
                    objectToAddToTeam.GetComponent<PickableItem>().PickItem(player.GetComponent<Inventory>());

                    if (ifHaveImpactOnMind == true && prefabToFindStats != null)
                    {
                        prefabToFindStats.mentalHealth += pointsToImpactTheMind;
                    }

               

                }
                else
                {
                    objectToAddToTeam.SetActive(false);
                    objectToAddToTeam.GetComponent<ObjectsManager>().setOff();


                    if (ifHaveImpactOnMind)
                    {
                        prefabToFindStats.mentalHealth -= pointsToImpactTheMind;
                    }
                }



            }
        }
       
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void GetObject(GameObject objectToDrop, GameObject objectWanted, string answerToGiveObject, string goodAnswer, bool canBeRecruited, bool haveToGiveObject, bool shouldOpenTheDoor, GameObject doorToOpen, GameObject objectToAddToTeam, bool ifHaveImpactOnMind, int pointsToImpactTheMind, Hero heroToStats)
    {

        this.objectToDrop = objectToDrop;
        this.objectWanted = objectWanted;
        this.answerToGiveTheObject = answerToGiveObject;
        this.goodAnswer = goodAnswer;
        this.canBeRecruited = canBeRecruited;
        this.haveToGiveObject = haveToGiveObject;
        this.shouldOpenTheDoor = shouldOpenTheDoor;
        this.doorToOpen = doorToOpen;
        this.objectToAddToTeam = objectToAddToTeam;
        this.ifHaveImpactOnMind = ifHaveImpactOnMind;
        this.pointsToImpactTheMind = pointsToImpactTheMind;
        this.prefabToFindStats = heroToStats;
    }
}
