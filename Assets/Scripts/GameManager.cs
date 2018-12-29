using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameIsOver;

    [Space] 
    public float startingPlayerSpeed;
    public float increaseInSpeed;
    public int speedIncreaseEvent;
    
    [Space]
    public int remainingPlayerCount;
    
    [Space]
    public float roundDuration;
    public float countdown;
    public float totalTime;

    [Space]
    public Transform playerCanvasParent;
    
    [Space]
    public Transform playerParent;
    private Player[] players;
    private Player realPlayer;
    
    [Space]
    public GameUI gameUI;
    public GameOverUI gameOverUI;
    
    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        GameAnalyticsSDK.GameAnalytics.Initialize(); 
    }

    void Start()
    {
        SetupPlayerArray();
        
        countdown = roundDuration;
        remainingPlayerCount = players.Length;
        totalTime = roundDuration * (remainingPlayerCount - 1);

        gameOverUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);

        gameIsOver = true;
        
        StartCoroutine(EliminationRoutine());
    }
    
    void Update()
    {
        if (gameIsOver)
        {
            return;
        }
        
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);
    }

    public Player[] GetPlayerArray()
    {
        return players;
    }

    public void EndGame(bool realPlayerWon)
    {
        gameIsOver = true;
        
        gameOverUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);

        if (realPlayerWon)
        {
            gameOverUI.SetGameOverText("You Won");
        }
        else
        {
            gameOverUI.SetGameOverText("Better Luck Next Time");
        }
        gameOverUI.ShowScoreboard(realPlayerWon);

        if (realPlayer.goals > PlayerPrefs.GetInt("best"))
        {
            PlayerPrefs.SetInt("best", realPlayer.goals);
            PlayerPrefs.Save();
        }
    }

    IEnumerator EliminationRoutine()
    {
        GameUI.Instance.ShowReadyMessage();
        yield return new WaitForSeconds(1.25f);
        GameUI.Instance.ShowGoMessage();
        yield return new WaitForSeconds(1.25f);
        GameUI.Instance.HideTutorial();

        gameIsOver = false;
        
        while (!gameIsOver)
        {
            yield return new WaitForSeconds(roundDuration);
            
            EliminatePlayer();
            countdown = roundDuration;
        }
    }

    private void SetupPlayerArray()
    {
        players = new Player[playerParent.childCount];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = playerParent.GetChild(i).GetComponent<Player>();
            
            if (players[i].isRealPlayer)
            {
                realPlayer = players[i];
            }
        }

        for (int i = 0; i < players.Length; i++)
        {
            playerCanvasParent.GetChild(i).GetComponent<NameTag>().SetPlayer(players[i]);
        }
    }

    private void EliminatePlayer()
    {
        BallManager.Instance.targetBallAmount--;
        GoalPostController.Instance.DisableGoalPost();
        Scoreboard.Instance.getEliminationPlayer().DisablePlayer();
        remainingPlayerCount--;

        if (remainingPlayerCount <= 1)
        {
            EndGame(true);
        }
    }
}
