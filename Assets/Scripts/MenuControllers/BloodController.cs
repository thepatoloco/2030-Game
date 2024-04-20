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
            transform.position = new Vector3(0, 0);
        }

        Vector2 targetPosition = new Vector2(0, -rectTransform.rect.height);

        StartCoroutine(moveObject(targetPosition));
    }

    private IEnumerator moveObject(Vector2 targetPosition)
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) > 0.01f)
        {
            Vector2 nextPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, targetPosition, ref velocity, transitionTime);
            transform.position = nextPosition;
            yield return null;
        }

        transform.position = targetPosition;
        velocity = Vector2.zero;
    }


    public void updated(Subject subject)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, baseHeight + aspectFitter.rect.height);
    }
}
