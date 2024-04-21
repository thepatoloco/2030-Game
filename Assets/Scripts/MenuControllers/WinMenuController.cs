using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenuController : MonoBehaviour, RestartGame, GameStop
{
    [SerializeField]
    private GameObject[] bgObjects;
    [SerializeField]
    private GameObject menuContent;
    [SerializeField]
    private float transitionTime = 3f;

    private Image[] bgImages;


    void Start()
    {
        List<Image> gettingBgImages = new List<Image>();
        foreach (GameObject bg in bgObjects)
        {
            gettingBgImages.Add(bg.GetComponent<Image>());
        }
        bgImages = gettingBgImages.ToArray();

        GameFacade.singleton.subscribeGameRestart(this);
        GameFacade.singleton.subscribeGameWon(this);
    }


    public void openMenu()
    {
        StartCoroutine(openingMenu());
    }

    public void closeMenu()
    {
        StopAllCoroutines();

        menuContent.SetActive(false);
        foreach (GameObject bg in bgObjects)
        {
            bg.SetActive(false);
        }
    }

    IEnumerator openingMenu()
    {
        for (int i=0; i<bgObjects.Length; i++)
        {
            bgObjects[i].SetActive(true);
            bgImages[i].color = new Color(bgImages[i].color.r, bgImages[i].color.g, bgImages[i].color.b, 0);
        }

        float waitingTime = 0f;

        while (waitingTime < transitionTime)
        {
            waitingTime += Time.deltaTime;

            foreach (Image bg in bgImages)
            {
                float transparency = (waitingTime / transitionTime);
                bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, Mathf.Min(transparency, 1f));
            }

            yield return null;
        }

        foreach (Image bg in bgImages)
        {
            bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 1f);
        }

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
