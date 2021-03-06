using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // GAME SETTINGS
    public List<GameObject> players;
    public List<GameObject> potions;
    private float spawnRate = 1.0f;
    public bool isGameActive;
    private int playerCount = -1;

    private List<PlayerStats> PlayerStatsList;
    private List<int> PlayerList;

    // ----- UI -----

    // TITLE
    public GameObject titleScreen;

    // CHOICE
    public GameObject PlayerChoiceScreen;
    public GameObject PlayerCountScreen;
    public List<GameObject> Choices;

    // IN GAME TEXT
    public TextMeshProUGUI BubbleStats;
    public TextMeshProUGUI IvyStats;
    public TextMeshProUGUI StormStats;
    public TextMeshProUGUI RoseStats;

    //public TextMeshProUGUI livesText;
    private int score;
    //public TextMeshProUGUI scoreText;
    public bool gamePaused;
    public TextMeshProUGUI pausedText;

    // GAME OVER TEXT
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    // CHOOSE PLAYER
    private Button IvyButton;
    private Button BubbleButton;
    private Button StormButton;
    private Button RoseButton;

    // Player Counter
    private Button Two;
    private Button Three;
    private Button Four;

    private int disabled=0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatsList = new List<PlayerStats>();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            gamePaused = !gamePaused;
            Pause();
        }

        if(disabled == playerCount) disableStartscreen();
    }

    public void StartGame()
    {
        isGameActive = true;
        titleScreen.SetActive(true);
        PlayerCounting();
    }

    private void SetPlayers(Button button)
    {
        playerCount = int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text);
        PlayerList = new List<int>(playerCount);
        PlayerCountScreen.SetActive(false);
        ChoosePlayer();
    }

    public void PlayerCounting()
    {
        PlayerCountScreen.gameObject.SetActive(true);

        Two = GameObject.Find("2").GetComponent<Button>();
        Three = GameObject.Find("3").GetComponent<Button>();
        Four = GameObject.Find("4").GetComponent<Button>();

        Two.onClick.AddListener(delegate { SetPlayers(Two); });
        Three.onClick.AddListener(delegate { SetPlayers(Three); });
        Four.onClick.AddListener(delegate { SetPlayers(Four); });
    }

    public void ChoosePlayer()
    {
        PlayerChoiceScreen.gameObject.SetActive(true);

        // -- Get Components
        IvyButton = GameObject.Find("Ivy").GetComponent<Button>();
        StormButton = GameObject.Find("Storm").GetComponent<Button>();
        BubbleButton = GameObject.Find("Bubble").GetComponent<Button>();
        RoseButton = GameObject.Find("Rose").GetComponent<Button>();

        IvyButton.onClick.AddListener(delegate { SetWizard(IvyButton, 0); });
        StormButton.onClick.AddListener(delegate { SetWizard(StormButton, 1); });
        BubbleButton.onClick.AddListener(delegate { SetWizard(BubbleButton, 2); });
        RoseButton.onClick.AddListener(delegate { SetWizard(RoseButton, 3); });
    }

    void Pause()
    {
        if (gamePaused)
        {
            Time.timeScale = 0;
            pausedText.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausedText.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);

        for(int i = 0; i < PlayerList.Count; i++)
        {
            ShowStats(PlayerStatsList[i].SendStats(PlayerList[i]), PlayerList[i]);
            Debug.Log("PLAYER IN LIST " + PlayerList[i]);
        }
        
    }

    public void ShowStats(string Stats, int playerId)
    {
        switch(playerId)
        {
            case 0:
                IvyStats.text = Stats;
                IvyStats.gameObject.SetActive(true);
                break;
            case 1:
                StormStats.text = Stats;
                StormStats.gameObject.SetActive(true);
                break;
            case 2:
                BubbleStats.text = Stats;
                BubbleStats.gameObject.SetActive(true);
                break;
            case 3:
                RoseStats.text = Stats;
                RoseStats.gameObject.SetActive(true);
                break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void addToPlayerList(PlayerStats Stats)
    { this.PlayerStatsList.Add(Stats); }

    public List<PlayerStats> getPlayerStatsList()
    { return PlayerStatsList; }

    public int getPlayerCount()
    { return playerCount;  }

    public void reducePlayerCount(string playerId)
    { 
        playerCount--; 
        GameObject playerToDie = GameObject.Find(playerId);
        Destroy(playerToDie);
    }

    private void SetWizard(Button button, int wizard)
    {
        button.interactable = false; // disable button
        Instantiate(players[wizard], new Vector3(wizard, 1, 0), players[wizard].transform.rotation);
        disabled++;
        PlayerList.Add(wizard);
    }

    private void disableStartscreen()
    {
        if (disabled == playerCount) PlayerChoiceScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
    }
}

