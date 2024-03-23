using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterController : MonoBehaviour
{
    private Image letterBg;
    [SerializeField]
    private TextMeshProUGUI textObject;

    private void Start()
    {
        letterBg = GetComponent<Image>();
    }

    public void setLetter(char letter)
    {
        textObject.text = letter.ToString();
    }

    public void removeLetter()
    {
        textObject.text = "";
    }
}
