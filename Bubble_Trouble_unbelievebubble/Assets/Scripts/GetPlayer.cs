using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayer : MonoBehaviour
{
    private GameManager gameManager;
    private Button button;

    public GameObject players;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetPlayer);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPlayer()
    {
        Instantiate(players, new Vector3(-2.29f, -3.94f, 3), players.transform.rotation);
        gameManager.StartGame();
    }
}
