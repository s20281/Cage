using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadableItem : MonoBehaviour
{
    
    [SerializeField] private GameObject readUI;
    [SerializeField] private TMP_Text textLabel;

    public void ReadStory(){
        readUI.SetActive(true);
        //textLabel.text = "AAAAAAAAAAAAAAAAAA";

    

}

    private void Update()
    {
        if (readUI.active && Input.GetKeyDown(KeyCode.Space))
        {
            readUI.SetActive(false);
            gameObject.SetActive(false);

            this.GetComponent<ObjectsManager>().setOff();
        }
    }
}
