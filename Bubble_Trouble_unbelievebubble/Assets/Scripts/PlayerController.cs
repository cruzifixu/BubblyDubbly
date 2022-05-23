using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // -- Player
    private Rigidbody playerRb;
    private bool isOnGround = true;
    private float gravityModifier = 0.8f;
    public int playerId;
    private PlayerStats playerStats;

    // -- Stats
    private float lives = 3;

    // --- Move
    private float horizontalInput;

    // --- Skills
    private float speed = 10.0f;
    private float BubbleSpeed = 10.0f;
    private float turnSpeed = 300.0f;
    private float jumpForce = 350;

    // --- Range
    private float PlusZRange = -3.4f;
    private float MinusZRange = -6;

    // --- bubbles
    public GameObject projectilePrefab;

    //private UpdatePlayerStats Stats;

    // Start is called before the first frame update
    void Start()
    {
        //Stats = GameObject.Find("PlayerStats").GetComponent<UpdatePlayerStats>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        int playerMoveId = playerId + 1;
        horizontalInput = Input.GetAxis("Horizontal" + playerMoveId);
        //verticalInput = Input.GetAxis("Vertical" + playerMoveId);

        //horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);
        movementDirection.Normalize();

        //transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        //transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed * -horizontalInput);

        //TODO: MAKE MOVEMENT BETTER (https://www.youtube.com/watch?v=BJzYGsMcy8Q&ab_channel=KetraGames)
        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }


        // player can not get off grounds
        if (transform.position.z < MinusZRange)
        { transform.position = new Vector3(transform.position.x, transform.position.y, MinusZRange); }
        if (transform.position.z > PlusZRange)
        { transform.position = new Vector3(transform.position.x, transform.position.y, PlusZRange); }

        // jump
        if (Input.GetAxis("Jump" + playerMoveId) ==1 && isOnGround)
        { jump(); } // returns clone of original
                    // shoot
        if (Input.GetAxis("Fire" + playerMoveId) ==1) //triangle
        { 
            shoot(); 
        } // returns clone of original
        //}

    }

    private void jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false; // prevent double jump
    }

    private void shoot()
    {
        Vector3 offset = new Vector3(0.5f, 1, 0);
        GameObject Bubble = Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
        Bubble.gameObject.tag = playerId.ToString();
        Rigidbody BubbleRb = Bubble.GetComponent<Rigidbody>();
        BubbleRb.AddForce(transform.right * BubbleSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if player is on Ground again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("0") || collision.gameObject.CompareTag("1") || collision.gameObject.CompareTag("2"))
        {
            if(collision.gameObject.tag != this.playerId.ToString())
            {
                Debug.Log(collision.gameObject.tag +" hit " + this.name);
                playerStats.reduceLivePercentage(collision.gameObject.tag, this.name);
                //Destroy(collision.gameObject);
            }

        }
    }
}
