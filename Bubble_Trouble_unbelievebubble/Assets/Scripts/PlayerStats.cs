using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Stats
    private float LivePercentage = 20f;
    private float Lives = 3;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -bound)
        { reduceLives();  }
    }

    public void reduceLives()
    { --this.Lives; }

    public void reduceLivePercentage(string playerId, string hurtPlayerId)
    {
        if (this.LivePercentage > 0)
        {
            this.LivePercentage--;
            Debug.Log(hurtPlayerId + "'s live = " + LivePercentage);
        }
        else
        {
            if(Lives < 1)
            {
                GameManagerScript.GameOver();
                Debug.Log(playerId + " won!");

                return;
            }
            reduceLives();
            Debug.Log(hurtPlayerId + " only has " + Lives + "left.");
        }
    }
}