using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFacade : MonoBehaviour
{
    public static GameFacade singleton;

    [SerializeField]
    private DeadMenuController deadMenuController;


    private List<RestartGame> restartGames = new List<RestartGame>();
    private List<GameStop> lostGames = new List<GameStop>();
    private List<GameStop> wonGames = new List<GameStop>();

    private GameDificulties gameDificulty = GameDificulties.Medium;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }

        singleton = this;
    }

    void Start()
    {
        switch (PlayerPrefs.GetString("selectedDifficulty"))
        {
            case "Easy":
                gameDificulty = GameDificulties.Easy;
                break;
            case "Hard":
                gameDificulty = GameDificulties.Hard;
                break;
            default:
                gameDificulty = GameDificulties.Medium;
                break;
        }

        gameRestart();
    }


    public void subscribeGameRestart(RestartGame restartGame)
    {
        restartGames.Add(restartGame);
    } 

    public void gameRestart()
    {
        foreach (RestartGame restart in restartGames)
        {
            restart.restartGame();
        }
    }

    public void subscribeGameLost(GameStop stopGame)
    {
        lostGames.Add(stopGame);
    }

    public void gameLost()
    {
        foreach (GameStop stop in lostGames)
        {
            stop.stopGame();
        }
    }

    public void subscribeGameWon(GameStop stopGame)
    {
        wonGames.Add(stopGame);
    }

    public void gameWon()
    {
        foreach (GameStop stop in wonGames)
        {
            stop.stopGame();
        }
    }

    public void openMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
