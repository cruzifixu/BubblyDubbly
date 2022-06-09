using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // -- Player
    private Rigidbody playerRb;
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
    private float KnockBack = 850f;
    private float jumpForce = 550;

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

        transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        if (horizontalInput != 0) playerAnim.SetTrigger("run_trig");
        else playerAnim.SetTrigger("stop_trig");

        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }


        // player can not get off grounds
        if (transform.position.z < MinusZRange)
        { transform.position = new Vector3(transform.position.x, transform.position.y, MinusZRange); }
        if (transform.position.z > PlusZRange)
        { transform.position = new Vector3(transform.position.x, transform.position.y, PlusZRange); }
        if(transform.position.y < -20)
        {
            this.playerRb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(-1.5f, 1, transform.position.z);
            playerStats.reduceLives();
        }

        // jump
        if (Input.GetAxis("Jump" + playerMoveId) ==1 && isOnGround)
        { 
            jump();
            playerAnim.SetTrigger("Jump_trig");
        } // returns clone of original
                    // shoot
        if (Input.GetAxis("Fire" + playerMoveId) ==1 && canShoot) //triangle
        { StartCoroutine(shootCou()); } // returns clone of original
    }

    private void jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false; // prevent double jump
    }

    public IEnumerator shootCou()
    {
        playerAnim.SetTrigger("Attack_trig");
        for (int x = 0; x < 10; x++) shoot();
        canShoot = false;
        yield return new WaitForSeconds(1);
        canShoot = true;
    }

    private void shoot()
    {
        string newTag = this.gameObject.name.Replace("(Clone)", "");
        Vector3 offset = new Vector3(0.5f, 1, 0);
        Vector3 left = new Vector3(-1, 0, 0);
        if (playerRb.transform.forward == left)
        { offset = new Vector3(-0.5f, 1, 0); }
        GameObject Bubble = Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
        Bubble.gameObject.tag = this.playerId.ToString();
        Bubble.gameObject.name = newTag + "_bubble";
        Rigidbody BubbleRb = Bubble.GetComponent<Rigidbody>();
        BubbleRb.AddForce(playerRb.transform.forward * BubbleSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if player is on Ground again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("0") || collision.gameObject.CompareTag("1") || collision.gameObject.CompareTag("2") || collision.gameObject.CompareTag("3"))
        {
            if(collision.gameObject.tag != this.playerId.ToString())
            {
                playerStats.reduceLivePercentage(collision.gameObject.name, this.name);
                Vector3 left = new Vector3(-1, 0, 0);
                if (collision.rigidbody.transform.forward == left)
                    this.playerRb.AddForce(-collision.transform.forward * KnockBack, ForceMode.Impulse);
                else
                    this.playerRb.AddForce(collision.transform.forward * KnockBack, ForceMode.Impulse);
                playerStats.playerHitOther();
                Destroy(collision.gameObject);
            }

        }
        //if collided with potion of various types
        if (collision.gameObject.CompareTag("health"))
        {
            playerStats.increaseLifes();
            Debug.Log("health potion");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("mana"))
        {
            playerStats.stopDamageForPeriod();
            Debug.Log("mana potion");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("endurance"))
        {
            //this potion is not actually beneficial -> makes you unabe to shoot for a bit
            canShoot = false;
            Invoke("enableShoot", 10);
            Debug.Log("endurance potion");
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator PlayerDeath(string playerId)
    {
        playerAnim.SetTrigger("Die");
        yield return new WaitForSeconds(1);
        GameObject playerToDie = GameObject.Find(playerId);
        Destroy(playerToDie);
    }
}
