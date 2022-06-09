using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Stats
    private float LivePercentage = 20f;
    private float Lives = 3;
    private float PlayerHit = 0;
    private float gotHit = 0;
    private bool canGetHurt = true;

    // Bounds
    private float bound = 30f;

    // Scripts
    private GameManager GameManagerScript;
    PlayerController playerControllerScript;

    // TEXTS
    public GameObject PlayerStatsScreen;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GetComponent<PlayerController>();
        GameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        GameManagerScript.addToPlayerList(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -bound)
        { reduceLives();  }
    }

    public void playerHitOther()
    { this.PlayerHit++; }

    public void reduceLives()
    { --this.Lives; }

    public void increaseLifes()
    {
        this.Lives++;
        Debug.Log("Life increased");
    }

    public void stopDamageForPeriod()
    {
        this.canGetHurt = false;
        Debug.Log("player stopped taking damage");
        Invoke("setCanGetHurt", 30);
    }

    private void setCanGetHurt()
    {
        this.canGetHurt = true;
        Debug.Log("player started taking damage agein");
    }

    public void reduceLivePercentage(string playerId, string hurtPlayerId)
    {
        playerId = playerId.Replace("_bubble", "");
        string newhurtPlayerId = hurtPlayerId.Replace("(Clone)", "");
        if (this.LivePercentage > 0)
        {
            this.LivePercentage--;
            gotHit++; // player got hit
            Debug.Log(playerId + " hit " + newhurtPlayerId);
        }
        else
        {
            if (this.Lives < 1 && canGetHurt == true)
            {
                canGetHurt = false;
                GameManagerScript.reducePlayerCount();
                StartCoroutine(playerControllerScript.PlayerDeath(hurtPlayerId));
                if (GameManagerScript.getPlayerCount() < 2) GameManagerScript.GameOver(playerId);
                return;
            }
            reduceLives();
            LivePercentage = 20f;
            Debug.Log(newhurtPlayerId + " only has " + Lives + " lives left.");
        }
    }

    public string SendStats(int playerId)
    {
        string player = "none";

        switch(playerId)
        {
            case 0:
                player = "Ivy";
                break;
            case 1:
                player = "Storm";
                break;
            case 2:
                player = "Bubble";
                break;
            case 3:
                player = "Rose";
                break;
        }
        string StatsText = player + "\nLives left: " + (3 - Lives) + "\nDamage taken: " + gotHit + "\nDamage made: " + PlayerHit;

        return StatsText;
    }

    public float getPlayerLives()
    { return Lives;  }
    
    public void getHurtChange(bool change)
    { this.canGetHurt = change; }

    public bool getHurt()
    { return canGetHurt; }
}