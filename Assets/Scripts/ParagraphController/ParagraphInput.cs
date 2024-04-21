using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParagraphInput : RiskSubject, RestartGame, GameStop
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

        GameFacade.singleton.subscribeGameRestart(this);
        GameFacade.singleton.subscribeGameLost(this);
        GameFacade.singleton.subscribeGameWon(this);
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


    public void restartGame()
    {
        SentenceSection[][] newParagraph = new SentenceSection[][]
        {
            new SentenceSection[]
            {
                new SentenceSection("Europe is the", false),
                new SentenceSection("enemy", true),
                new SentenceSection("of the nation.", false)
            },
            new SentenceSection[]
            {
                new SentenceSection("No matter what we did, this is still true.", false)
            },
            new SentenceSection[]
            {
                new SentenceSection("We will continue the", false),
                new SentenceSection("battle", true),
                new SentenceSection(".", false)
            }
        };

        isShowing = false;
        toggleText.text = "Show";
        gameRunning = true;
        paperMovement.resetActivation();
        paragraphController.setParagraph(newParagraph);
        notify();
    }

    public void stopGame()
    {
        gameRunning = false;
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

        if (paragraphController.checkCorrect())
        {
            Debug.Log("Correct answer!");
            GameFacade.singleton.gameWon();
        }
    }

    private bool canInput()
    {
        return gameRunning && isShowing && (!paperMovement.isMoving() || paperMovement.distanceFromTarget() < targetDistanceAccept);
    }

    public override bool isActive()
    {
        return isShowing;
    }
}
