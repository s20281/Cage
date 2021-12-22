using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;
    private Vector3 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        moveVelocity = Vector3.zero;


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;

        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            moveVelocity = Vector3.up;

        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            moveVelocity = Vector3.down;

        }

        transform.up = moveVelocity;
        transform.position += moveVelocity * speed * Time.deltaTime;


}
}
