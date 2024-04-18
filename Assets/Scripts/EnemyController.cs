using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, Observer
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float ticTime = 0.1f;
    [SerializeField]
    private float moveProbability = 0.03f;
    [SerializeField]
    private float backProbability = 0.2f;
    [SerializeField]
    private float movementLagMin = 3.5f;
    [SerializeField]
    private float movementLagMax = 6f;

    private SpriteRenderer spriteRenderer;

    private int enemyStage = 0;
    private float actualTime = 0f;
    private float lagTime = 0f;
    private int riskLevel = 0;

    private void Start()
    {
        RiskController.singleton.subscribe(this);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (lagTime > 0f && riskLevel > 0)
        {
            lagTime -= Time.deltaTime;
            return;
        }

        actualTime += Time.deltaTime * getRiskMultiplier();
        while (actualTime >= ticTime)
        {
            if (riskLevel <= 0)
            {
                if (Random.value < backProbability) setStage(enemyStage - 1); 
            }
            else
            {
                if (Random.value < moveProbability) setStage(enemyStage + 1);
            }

            actualTime -= ticTime;
        }
    }


    private void setStage(int newStage)
    {
        if (newStage < 0) return;
        if (newStage >= sprites.Length)
        {
            Debug.Log("You are dead!");
            return;
        }

        enemyStage = newStage;
        spriteRenderer.sprite = sprites[enemyStage];
        lagTime = Random.Range(movementLagMin, movementLagMax);
    }

    private float getRiskMultiplier()
    {
        return ((Mathf.Max(1f, riskLevel) - 1) * 0.25f) + 1f;
    }


    public void updated(Subject subject)
    {
        riskLevel = (subject as RiskController).getRiskLevel();
    }
}
