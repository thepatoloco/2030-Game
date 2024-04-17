using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSubject : MonoBehaviour, Subject
{
    [SerializeField]
    private RectTransform canvas;

    private int[] resolution = new int[2];
    private Vector2 canvasSize = new Vector2();

    private List<Observer> observers = new List<Observer>();

    private void Start()
    {
        resolution[0] = Screen.width;
        resolution[1] = Screen.height;
        canvasSize = new Vector2(canvas.rect.width, canvas.rect.height);

        notify();
    }

    private void Update()
    {
        if (resolution[0] != Screen.width || resolution[1] != Screen.height || canvasSize.x != canvas.rect.width || canvasSize.y != canvas.rect.height)
        {
            resolution[0] = Screen.width;
            resolution[1] = Screen.height;
            canvasSize = new Vector2(canvas.rect.width, canvas.rect.height);

            Debug.Log("Screen resolution change!");
            notify();
        }
    }


    public void subscribe(Observer observer)
    {
        observers.Add(observer);
    }

    public void unsubscribe(Observer observer)
    {
        observers.Remove(observer);
    }

    public void notify()
    {
        foreach (Observer obs in observers)
        {
            obs.updated(this);
        }
    }


    public int[] getResolution()
    {
        return resolution;
    }

    public Vector2 getCanvasSize()
    {
        return canvasSize;
    }
}
