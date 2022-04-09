using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rb;
    private Vector3 moveVelocity;
    Inventory inventory;
    bool canPress = false;
    Collider2D collider2d;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();

        if(PlayerPrefs.HasKey("PlayerPostionX"))
        {
            Vector3 vec = new Vector3(PlayerPrefs.GetFloat("PlayerPostionX"), PlayerPrefs.GetFloat("PlayerPostionY"));
            this.transform.position = vec;
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("PlayerPostionX", this.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPostionY", this.transform.position.y);

        if (SceneManager.GetActiveScene().name == "Level 1")
        {

            
        }
        else
        {
            
        }

        Collider2D collider2dold = collider2d; 

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

        if(collider2dold != collider2d)
        {
            canPress = true;
        }

        if (canPress)
        {         

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (collider2d)
                {
                    Interactable interactable = collider2d.GetComponent<Interactable>();

                    if (interactable)
                    {

                        interactable.startInteraction(inventory);

                    }
                }
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canPress = true;
        collider2d = collision;

        FindGameObjectWithInteractionTag(collision);
      

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
        FindGameObjectWithInteractionTag(collision);
    }

    private void FindGameObjectWithInteractionTag(Collider2D collision)
    {
        Transform interactionTag = collision.gameObject.transform;
        Transform findInteractionTag = interactionTag.Find("interactionTag");
        if (findInteractionTag != null)
        {

            if (findInteractionTag.gameObject.name == "interactionTag")
            {

                if (findInteractionTag.gameObject.activeInHierarchy == false)
                {
                    findInteractionTag.gameObject.SetActive(true);
                }
                else
                {
                    findInteractionTag.gameObject.SetActive(false);
                }
            }

        }

    }
}
