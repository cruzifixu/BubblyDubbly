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
    private float playerCount;

    // ----- UI -----

    // TITLE
    public GameObject titleScreen;

    // CHOICE
    public GameObject PlayerChoiceScreen;
    public List<GameObject> Choices;

    // IN GAME TEXT


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

    private int disabled=0;

    // Start is called before the first frame update
    void Start()
    {
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

        if(disabled > 1) disableStartscreen();
    }

    public void StartGame()
    {
        isGameActive = true;
        //StartCoroutine(SpawnTarget());
        spawnRate /= playerCount;

        ChoosePlayer();
    }

    public void ChoosePlayer()
    {
        PlayerChoiceScreen.gameObject.SetActive(true);

        int disabled = 0;

        // -- Get Components
        IvyButton = GameObject.Find("Ivy").GetComponent<Button>();
        BubbleButton = GameObject.Find("Bubble").GetComponent<Button>();
        StormButton = GameObject.Find("Storm").GetComponent<Button>();

        IvyButton.onClick.AddListener(SetIvy);
        BubbleButton.onClick.AddListener(SetBubble);
        StormButton.onClick.AddListener(SetStorm);

        /*
        if (!IvyButton.enabled) disabled += 1;
        if (!BubbleButton.enabled) disabled += 1;
        if (!StormButton.enabled) disabled += 1;

        if (disabled > 1) PlayerChoiceScreen.gameObject.SetActive(false);
        */
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score < 0)
        {
            score = 0;
            GameOver();
        }
        //scoreText.text = "Score: " + score;
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
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, potions.Count);
            Instantiate(potions[index]);
        }

    }

    private void SetIvy()
    {
        IvyButton.interactable = false; // disable Button
        Instantiate(players[0], new Vector3(1, 1, 0), players[0].transform.rotation);
        disabled++;
    }
    private void SetBubble()
    {
        BubbleButton.interactable = false; // disable Button
        Instantiate(players[1], new Vector3(2, 1, 0), players[1].transform.rotation);
        disabled++;
    }

    private void SetStorm()
    {
        StormButton.interactable = false; // disable Button
        Instantiate(players[2], new Vector3(3, 1, 0), players[2].transform.rotation);
        disabled++;
    }

    private void disableStartscreen()
    {
        if (disabled > 1) PlayerChoiceScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
    }
}

