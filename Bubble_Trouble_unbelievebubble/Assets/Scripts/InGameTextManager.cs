using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTextManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitPlayer(string player1, string player2)
    { Debug.Log(player1 + " hit " + player2); }

    public void playerLives(string player, int lives)
    { Debug.Log(player + " only has " + lives + "left."); }

    public string Winner(string player)
    { return player + "won!"; }
}
