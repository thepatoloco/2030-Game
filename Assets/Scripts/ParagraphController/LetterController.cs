using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterController : MonoBehaviour
{
    [SerializeField]
    private Color idleColor;
    [SerializeField]
    private Color correctColor;

    private Image letterBg;
    [SerializeField]
    private TextMeshProUGUI textObject;


    public enum letterStates { idle, correct };
    private letterStates state = letterStates.idle;

    private void Start()
    {
        letterBg = GetComponent<Image>();
        removeLetter();
        setState(letterStates.idle);
    }

    public void setLetter(char letter)
    {
        textObject.text = letter.ToString();
    }

    public char getLetter()
    {
        return textObject.text[0];
    }

    public void removeLetter()
    {
        textObject.text = " ";
    }

    public bool isCorrect()
    {
        return state == letterStates.correct;
    }

    public void setState(letterStates newState)
    {
        state = newState;
        switch(newState)
        {
            case letterStates.idle:
                letterBg.color = idleColor;
                break;
            case letterStates.correct:
                letterBg.color = correctColor;
                break;
        }
    }
}
