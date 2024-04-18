using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class RiskModifier
{
    public RiskSubject riskSubject;
    public int riskModifier;
}

public class RiskController : MonoBehaviour, Observer, Subject
{
    public static RiskController singleton;

    [SerializeField]
    private GameObject[] riskLevels;
    [SerializeField]
    private int baseLevel = 1;
    [SerializeField]
    private RiskModifier[] riskModifiers = new RiskModifier[0];

    private int riskLevel = 1;
    private List<Observer> observers = new List<Observer>();


    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;

        foreach (RiskModifier riskmod in riskModifiers)
        {
            riskmod.riskSubject.subscribe(this);
        }

        calcRiskLevel();
    }

    private void Start()
    {
        notify();
    }


    public void calcRiskLevel()
    {
        int newRiskLevel = baseLevel;
        foreach (RiskModifier riskMod in riskModifiers)
        {
            if (riskMod.riskSubject.isActive())
            {
                newRiskLevel += riskMod.riskModifier;
            }
        }

        riskLevel = newRiskLevel;

        showRiskLevel();
        notify();
    }

    public void showRiskLevel()
    {
        for (int i=0; i<riskLevels.Length; i++)
        {
            riskLevels[i].SetActive(i <= riskLevel);
        }
    }

    public int getRiskLevel()
    {
        return riskLevel;
    }


    public void updated(Subject subject)
    {
        calcRiskLevel();
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
}
