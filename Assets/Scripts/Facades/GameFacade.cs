using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour, Subject
{
    public static GameFacade singleton;

    private GameStatus gameStatus = GameStatus.Inactive;
    private GameDificulties gameDificulty = GameDificulties.Medium;

    private List<Observer> observers = new List<Observer>();

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
        gameStatus = GameStatus.Active;

        notify();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void subscribe(Observer observer)
    {
        observers.Add(observer);
    }

    public void unsubscribe(Observer observer)
    {
        observers.Remove(observer);
    }

    public void notify()
    {
        foreach (Observer obs in observers)
        {
            obs.updated(this);
        }
    }

    public GameStatus GetGameStatus()
    {
        return gameStatus;
    }
}
