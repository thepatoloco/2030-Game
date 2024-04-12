using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WrongLetterController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textObject;

    public void setLetter(char letter)
    {
        textObject.text = letter.ToString();
    }
}
