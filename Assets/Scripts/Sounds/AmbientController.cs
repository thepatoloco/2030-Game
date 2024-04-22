using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientController : MonoBehaviour, RestartGame, GameStop
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameFacade.singleton.subscribeGameRestart(this);
        GameFacade.singleton.subscribeGameLost(this);
        GameFacade.singleton.subscribeGameWon(this);
    }


    public void restartGame()
    {
        audioSource.Play();
    }

    public void stopGame()
    {
        audioSource.Stop();
    }
}
