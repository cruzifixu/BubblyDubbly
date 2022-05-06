using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float lives = 100f;
    private GameManager GameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void reduceLives()
    {
        if (GameManagerScript == null) Debug.Log("manager null");
        if(this.lives > 0)
        {
            this.lives--;
            Debug.Log(lives);
        }
        else GameManagerScript.GameOver();
    }
}