using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour, RestartGame, GameStop
{
    [SerializeField]
    private float shortTimeProb = 0.66f;
    [SerializeField]
    private float shortTimeMin = 0.1f;
    [SerializeField]
    private float shortTimeMax = 1.1f;
    [SerializeField]
    private float longTimeMin = 1f;
    [SerializeField]
    private float longTimeMax = 3f;
    [SerializeField]
    private float lightDuration = 0.15f;
    [SerializeField]
    private AudioClip[] audioClips;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private float waitingTime = 0.5f;
    private float transparency = 0f;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        GameFacade.singleton.subscribeGameRestart(this);
        GameFacade.singleton.subscribeGameLost(this);
        GameFacade.singleton.subscribeGameWon(this);
    }

    private void Update()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Max(0, transparency));
        if (transparency > 0)
        {
            transparency -= Time.deltaTime / lightDuration;
        }

        waitingTime -= Time.deltaTime;

        if (waitingTime <= 0)
        {
            transparency = 1f;
            if (Random.value < shortTimeProb)
            {
                waitingTime = Random.Range(shortTimeMin, shortTimeMax);
            }
            else
            {
                waitingTime = Random.Range(longTimeMin, longTimeMax);
            }
            playSound();
        }
    }


    private void playSound()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }


    public void restartGame()
    {
        audioSource.volume = 0.08f;
    }

    public void stopGame()
    {
        audioSource.volume = 0f;
    }
}
