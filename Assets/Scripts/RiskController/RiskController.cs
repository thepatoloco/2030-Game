using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class RiskModifier
{
    public RiskSubject riskSubject;
    public int riskModifier;
}

public class RiskController : MonoBehaviour, Observer
{
    [SerializeField]
    private GameObject[] riskLevels;
    [SerializeField]
    private int baseLevel = 1;
    [SerializeField]
    private RiskModifier[] riskModifiers = new RiskModifier[0];

    private int riskLevel = 1;


    private void Awake()
    {
        foreach (RiskModifier riskmod in riskModifiers)
        {
            riskmod.riskSubject.subscribe(this);
        }

        calcRiskLevel();
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
    }

    public void showRiskLevel()
    {
        for (int i=0; i<riskLevels.Length; i++)
        {
            riskLevels[i].SetActive(i <= riskLevel);
        }
    }


    public void updated(Subject subject)
    {
        calcRiskLevel();
    }
}
