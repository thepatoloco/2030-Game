using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BloodController : MonoBehaviour, Observer
{
    [SerializeField]
    private RectTransform aspectFitter;
    [SerializeField]
    private float transitionTime = 5f;


    private RectTransform rectTransform;

    private float baseHeight = 0f;
    private Vector2 velocity = Vector2.zero;
    private Vector2 activePosition = Vector2.zero;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        baseHeight = rectTransform.sizeDelta.y;

        ResolutionSubject.singleton.subscribe(this);
    }

    public void setActivation(bool active)
    {
        if (!active)
        {
            StopAllCoroutines();
            rectTransform.anchoredPosition = Vector2.zero;
            return;
        }

        StartCoroutine(moveObject());
    }

    private IEnumerator moveObject()
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, activePosition) > 0.01f)
        {
            Vector2 nextPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, activePosition, ref velocity, transitionTime);
            rectTransform.anchoredPosition = nextPosition;
            yield return null;
        }

        rectTransform.anchoredPosition = activePosition;
        velocity = Vector2.zero;
    }

    public float getTransitionTime()
    {
        return transitionTime;
    }


    public void updated(Subject subject)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, baseHeight + aspectFitter.rect.height);
        activePosition = new Vector2(0, -rectTransform.sizeDelta.y);
    }
}
