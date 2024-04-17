using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBgFit : MonoBehaviour, Observer
{
    [SerializeField]
    private ResolutionSubject resolutionSubject;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        resolutionSubject.subscribe(this);
    }

    public void updated(Subject subject)
    {
        Vector2 canvasSize = resolutionSubject.getCanvasSize();

        rect.sizeDelta = canvasSize;
    }
}
