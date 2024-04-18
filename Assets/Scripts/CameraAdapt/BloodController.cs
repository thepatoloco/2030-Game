using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour, Observer
{
    [SerializeField]
    private RectTransform aspectFitter;

    private RectTransform rectTransform;

    private float baseHeight = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        baseHeight = rectTransform.sizeDelta.y;

        ResolutionSubject.singleton.subscribe(this);
    }

    public void updated(Subject subject)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, baseHeight + aspectFitter.rect.height);
    }
}
