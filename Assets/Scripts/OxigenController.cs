using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxigenController : RiskSubject, RestartGame, GameStop
{
    [SerializeField]
    private float breathDuration = 5f;
    [SerializeField]
    private float rechargePunish = 0.7f;
    [SerializeField]
    private RectTransform barTransform;

    private float barBaseWidth;
    private float breathLevel = 1f;
    private float rechargeMultiplier = 1f;
    private bool buttonHold = false;
    private bool canHold = true;


    void Start()
    {
        barBaseWidth = barTransform.rect.width;

        GameFacade.singleton.subscribeGameRestart(this);
        GameFacade.singleton.subscribeGameLost(this);
        GameFacade.singleton.subscribeGameWon(this);
    }

    void Update()
    {
        if (buttonHold && canHold)
        {
            breathLevel -= Time.deltaTime / breathDuration;

            if (breathLevel <= 0f)
            {
                breathLevel = 0f;
                StartCoroutine(cantHoldTimer());
            }
        }
        else if (breathLevel < 1f)
        {
            breathLevel += (Time.deltaTime / breathDuration) * rechargeMultiplier;

            if (breathLevel > 1f) breathLevel = 1f;
        }

        showBreathLevel();
    }


    private void showBreathLevel()
    {
        barTransform.sizeDelta = new Vector2(breathLevel * barBaseWidth, barTransform.sizeDelta.y);
    }

    IEnumerator cantHoldTimer()
    {
        canHold = false;
        rechargeMultiplier = rechargePunish;
        notify();

        while (breathLevel < 1f)
        {
            yield return null;
        }

        canHold = true;
        rechargeMultiplier = 1f;
        notify();
        breathLevel = 1f;
    }

    public void setHold(bool hold)
    {
        buttonHold = hold;
        notify();
    }

    public void restartGame()
    {
        breathLevel = 1f;
        rechargeMultiplier = 1f;
        canHold = true;
    }

    public void stopGame()
    {
        StopAllCoroutines();
        canHold = false;
    }


    public override bool isActive()
    {
        return buttonHold && canHold;
    }
}
