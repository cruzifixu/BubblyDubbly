using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // -- Player
    private Rigidbody playerRb;
    private bool isOnGround = true;
    private float gravityModifier = 0.8f;

    // --- Move
    private float verticalInput;
    private float horizontalInput;

    // --- Skills
    private float speed = 10.0f;
    private float turnSpeed = 100.0f;
    private float jumpForce = 400;

    // --- Range
    private float xRange = 18;
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
    }

    // Update is called once per frame
    void Update()
    {
        //if (Stats.getHealth() > 0)
        //{

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * -horizontalInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);


        /* player can not get off grounds
        if (transform.position.x < -xRange)
        { transform.position = new Vector3(-xRange, transform.position.y, transform.position.z); }
        if (transform.position.x > xRange)
        { transform.position = new Vector3(xRange, transform.position.y, transform.position.z); }

        */

            // player can not get off grounds
            if (transform.position.z < MinusZRange)
            { transform.position = new Vector3(transform.position.x, transform.position.y, MinusZRange); }
            if (transform.position.z > PlusZRange)
            { transform.position = new Vector3(transform.position.x, transform.position.y, PlusZRange); }

            // jump
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            { jump(); } // returns clone of original
            // shoot
            if (Input.GetKeyDown(KeyCode.E))
            { shoot(); } // returns clone of original
        //}

    }

    public Vector3 getPlayerRotation()
    { return transform.position; }

    private void jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false; // prevent double jump
    }

    private void shoot()
    {
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if player is on Ground again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        
    }
}
