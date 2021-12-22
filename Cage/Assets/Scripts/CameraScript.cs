using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{
    
    public Transform playerTransform;
    private float cameraOffset = -10f;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraOffset);
        
    }
}
