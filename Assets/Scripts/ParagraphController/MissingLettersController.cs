using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingLettersController : MonoBehaviour
{
    [SerializeField]
    private GameObject wrongLetterObject;

    public void setMissingLetters(List<char> missingLetters)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (char letter in missingLetters)
        {
            GameObject newLetterObject = Instantiate(wrongLetterObject, transform.position, Quaternion.identity);
            newLetterObject.transform.SetParent(transform, false);
            newLetterObject.GetComponent<WrongLetterController>().setLetter(letter);
        }
    }
}
