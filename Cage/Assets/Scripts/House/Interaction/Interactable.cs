using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{


    public void startInteraction(Inventory inventory)
    {

        if (gameObject.GetComponent<OpenableItem>())
        {

            gameObject.GetComponent<OpenableItem>().OpenIt(inventory);
        }

        if (gameObject.GetComponent<ReadableItem>())
        {
            gameObject.GetComponent<ReadableItem>().ReadStory();
        }

        if (gameObject.GetComponent<TalkableCharacter>())
        {
            gameObject.GetComponent<TalkableCharacter>().starTalking();

        }
        else
        {
            if (gameObject.GetComponent<PickableItem>())
            {
                gameObject.GetComponent<PickableItem>().PickItem(inventory);
            }
        }





    }
}
