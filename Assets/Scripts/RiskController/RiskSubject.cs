using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RiskSubject : MonoBehaviour, Subject
{
    private List<Observer> observers = new List<Observer>();

    public virtual void subscribe(Observer observer)
    {
        observers.Add(observer);
    }

    public virtual void unsubscribe(Observer observer)
    {
        observers.Remove(observer);
    }

    public virtual void notify()
    {
        foreach (Observer obs in observers)
        {
            obs.updated(this);
        }
    }

    public abstract bool isActive();
}
