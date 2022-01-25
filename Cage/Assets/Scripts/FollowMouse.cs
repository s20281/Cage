using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
       
    }
}
