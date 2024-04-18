using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBarsFit : MonoBehaviour, Observer
{
    [SerializeField]
    private RectTransform aspecFitter;
    [SerializeField]
    private RectTransform leftBar;
    [SerializeField]
    private RectTransform rightBar;
    [SerializeField]
    private RectTransform topBar;
    [SerializeField]
    private RectTransform bottomBar;

    private RectTransform rectTransform;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        ResolutionSubject.singleton.subscribe(this);
    }


    public void updated(Subject subject)
    {
        float horizontalSize = (rectTransform.rect.width - aspecFitter.rect.width) / 2;
        float verticalSize = (rectTransform.rect.height - aspecFitter.rect.height) / 2;

        leftBar.sizeDelta = new Vector2(horizontalSize, leftBar.sizeDelta.y);
        rightBar.sizeDelta = new Vector2(horizontalSize, rightBar.sizeDelta.y);
        topBar.sizeDelta = new Vector2(topBar.sizeDelta.x, verticalSize);
        bottomBar.sizeDelta = new Vector2(bottomBar.sizeDelta.x, verticalSize);
    }
}
