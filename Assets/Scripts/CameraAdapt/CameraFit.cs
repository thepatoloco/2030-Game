using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFit : MonoBehaviour, Observer
{
    [SerializeField]
    private Vector2 gameAreaDimentions;
    [SerializeField]
    private CanvasScaler canvasScaler;
    [SerializeField]
    private ResolutionSubject resolutionSubject;

    private float targetAspect = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        targetAspect = gameAreaDimentions.x / gameAreaDimentions.y;

        resolutionSubject.subscribe(this);
    }

    public void updated(Subject subject)
    {
        int[] resolution = resolutionSubject.getResolution();
        float windowaspect = (float)resolution[0] / (float)resolution[1];

        if (windowaspect <= targetAspect)
        {
            float orthoSize = gameAreaDimentions.x * (float)resolution[1] / (float)resolution[0] * 0.5f;

            Camera.main.orthographicSize = orthoSize;

            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            Camera.main.orthographicSize = 5;

            canvasScaler.matchWidthOrHeight = 1;
        }
    }
}
