using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParagraphInput : RiskSubject, Observer
{
    [SerializeField]
    private float targetDistanceAccept = 0.05f;

    [SerializeField]
    private ParagraphController paragraphController;
    private PaperMovement paperMovement;
    [SerializeField]
    private TextMeshProUGUI toggleText;

    private bool isShowing = false;
    private bool gameRunning = false;


    void Start()
    {
        paperMovement = GetComponent<PaperMovement>();
    }

    void Update()
    {
        if (!canInput()) return;

        if (Input.anyKeyDown && Input.inputString.Length > 0 && char.IsLetter(Input.inputString[0]))
        {
            Debug.Log("Tecla presionada: " + Input.inputString);
            paragraphController.addLetter(char.ToLower(Input.inputString[0]));
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Eliminar letra.");
            paragraphController.removeLetter();
        }
    }


    public void toggleShow()
    {
        if (!gameRunning) return;
        if (paperMovement.isMoving()) return;

        isShowing = !isShowing;
        paperMovement.setActivation(isShowing);
        toggleText.text = isShowing ? "Hide" : "Show";

        notify();
    }

    public void submit()
    {
        if (!canInput()) return;

        if (paragraphController.checkCorrect()) Debug.Log("Correct answer!");
    }

    private bool canInput()
    {
        return gameRunning && isShowing && (!paperMovement.isMoving() || paperMovement.distanceFromTarget() < targetDistanceAccept);
    }

    public override bool isActive()
    {
        return isShowing;
    }


    public void updated(Subject subject)
    {
        gameRunning = (subject as GameFacade).GetGameStatus() == GameStatus.Active;
    }
}
