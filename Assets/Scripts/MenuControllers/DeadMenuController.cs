using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMenuController : MonoBehaviour, RestartGame, GameStop
{
    [SerializeField]
    private BloodController bloodController;
    [SerializeField]
    private GameObject menuContent;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameFacade.singleton.subscribeGameRestart(this);
        GameFacade.singleton.subscribeGameLost(this);
    }


    public void openMenu()
    {
        StartCoroutine(openingMenu());
    }

    public void closeMenu()
    {
        StopAllCoroutines();

        menuContent.SetActive(false);
        bloodController.setActivation(false);
    }

    IEnumerator openingMenu()
    {
        audioSource.Play();

        yield return new WaitForSeconds(1f);

        bloodController.setActivation(true);

        yield return new WaitForSeconds(bloodController.getTransitionTime() + 1f);

        menuContent.SetActive(true);
    }


    public void restartGame()
    {
        closeMenu();
    }

    public void stopGame()
    {
        openMenu();
    }
}
