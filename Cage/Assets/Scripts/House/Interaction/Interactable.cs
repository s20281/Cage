using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    

    
    public void startInteraction(Inventory inventory)
    {
      
            if (gameObject.GetComponent<PickableItem>())
            {
                gameObject.GetComponent<PickableItem>().PickItem(inventory);
            }

            if (gameObject.GetComponent<OpenableItem>())
            {

                gameObject.GetComponent<OpenableItem>().OpenIt(inventory);
            }

            if (gameObject.GetComponent<ReadableItem>())
            {

            }

            if (gameObject.GetComponent<TalkableCharacter>())
            {

            }
        
    }
}
