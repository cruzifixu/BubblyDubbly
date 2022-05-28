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

    // TEXTS
    public GameObject PlayerStatsScreen;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void stopDamageForPeriod()
    {
        this.canGetHurt = false;
        Invoke("setCanGetHurt", 30);
    }

    private void setCanGetHurt()
    {
        this.canGetHurt = true;
    }



    public void reduceLivePercentage(string playerId, string hurtPlayerId)
    {
        if (this.LivePercentage > 0)
        {
            this.LivePercentage--;
            gotHit++; // player got hit
            Debug.Log(playerId + " hit " + hurtPlayerId);
        }
        else
        {
            if(Lives < 1)
            {
                GameManagerScript.GameOver();
                Debug.Log("player " + playerId + " won");
                return;
            }
            reduceLives();
            Debug.Log(playerId + " only has " + Lives + "left.");
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
        }
        string StatsText = player + "\nLives left: " + (3 - Lives) + "\nTook damage: " + gotHit + "\nDamage made: " + PlayerHit;

        return StatsText;
    }
}