using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // -- Player
<<<<<<< Updated upstream
    private Rigidbody playerRb;
=======
    private Rigidbody playerRb; 
>>>>>>> Stashed changes
    private Animator playerAnim;
    private bool isOnGround = true;
    private float gravityModifier = 1.2f;
    public int playerId;
    private PlayerStats playerStats;
    private bool canShoot = true;

    // -- Stats
    private float lives = 3;

    // --- Move
    private float horizontalInput;

    // --- Skills
    private float speed = 10.0f;
    private float BubbleSpeed = 30.0f;
    private float turnSpeed = 300.0f;
<<<<<<< Updated upstream
    private float jumpForce = 400;
=======
    private float jumpForce = 450;
>>>>>>> Stashed changes

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
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int playerMoveId = playerId + 1;
        horizontalInput = Input.GetAxis("Horizontal" + playerMoveId);

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
<<<<<<< Updated upstream
        { 
            jump();
            /*playerAnim.SetTrigger("JumpStart");
            playerAnim.SetTrigger("JumpUp");
            playerAnim.SetTrigger("JumpAir");
            playerAnim.SetTrigger("JumpEnd");*/
        } // returns clone of original
                    // shoot
        if (Input.GetAxis("Fire" + playerMoveId) ==1 && canShoot) //triangle
        { 
            shoot(); 
=======
        {  jump(); } // returns clone of original
                     // shoot
        if (Input.GetAxis("Fire" + playerMoveId) == 1 && canShoot) //triangle
        {
            StartCoroutine(shootCou());
>>>>>>> Stashed changes
        } // returns clone of original
        //}

    }

    private void jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false; // prevent double jump
        playerAnim.SetTrigger("Jump_trig");
        //playerAnim.SetTrigger("JumpUp");
        //playerAnim.SetTrigger("JumpAir");
        //playerAnim.SetTrigger("JumpEnd");
    }

    public IEnumerator shootCou()
    {
        shoot();
        canShoot = false;
        yield return new WaitForSeconds(1);
        canShoot = true;
    }

    private void shoot()
    {
        Vector3 offset = new Vector3(0.5f, 1, 0);
        Vector3 left = new Vector3(-1, 0, 0);
        if (playerRb.transform.forward == left)
        { offset = new Vector3(-0.5f, 1, 0); }
        GameObject Bubble = Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
        Bubble.gameObject.tag = playerId.ToString();
        Rigidbody BubbleRb = Bubble.GetComponent<Rigidbody>();
<<<<<<< Updated upstream
        //BubbleRb.AddForce(transform.right * BubbleSpeed);
        BubbleRb.AddForce(playerRb.transform.forward * BubbleSpeed);
        Debug.Log(playerRb.transform.forward);
    }

    private void enableShoot()
    {
        this.canShoot = true;
        Debug.Log("shooting of " + playerId + " enabled");
=======
        BubbleRb.AddForce(playerRb.transform.forward * BubbleSpeed, ForceMode.Impulse);
>>>>>>> Stashed changes
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if player is on Ground again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        //if collided with bubble
        if(collision.gameObject.CompareTag("0") || collision.gameObject.CompareTag("1") || collision.gameObject.CompareTag("2"))
        {
            if(collision.gameObject.tag != this.playerId.ToString())
            {
                Debug.Log(collision.gameObject.name +" hit " + this.name);
                playerStats.reduceLivePercentage(collision.gameObject.tag, this.name);
                playerStats.playerHitOther();
                Destroy(collision.gameObject);
            }

        }
        //if collided with potion of various types
<<<<<<< Updated upstream
        if(collision.gameObject.CompareTag("health"))
=======
        if (collision.gameObject.CompareTag("health"))
>>>>>>> Stashed changes
        {
            playerStats.increaseLifes();
            Debug.Log("health potion");
            Destroy(collision.gameObject);
        }
<<<<<<< Updated upstream
        if(collision.gameObject.CompareTag("mana"))
=======
        if (collision.gameObject.CompareTag("mana"))
>>>>>>> Stashed changes
        {
            playerStats.stopDamageForPeriod();
            Debug.Log("mana potion");
            Destroy(collision.gameObject);
        }
<<<<<<< Updated upstream
        if(collision.gameObject.CompareTag("endurance"))
=======
        if (collision.gameObject.CompareTag("endurance"))
>>>>>>> Stashed changes
        {
            //this potion is not actually beneficial -> makes you unabe to shoot for a bit
            canShoot = false;
            Invoke("enableShoot", 30);
            Debug.Log("endurance potion");
            Destroy(collision.gameObject);
        }
    }
}
