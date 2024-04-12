using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WordController : MonoBehaviour
{
    [SerializeField]
    private GameObject letterObject;

    private string baseWord = "";
    private LetterController[] letters = new LetterController[0];


    public void setBaseWord(string word)
    {
        baseWord = word;
        letters = new LetterController[word.Length];

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i=0; i< baseWord.Length; i++)
        {
            GameObject newLetterObject = Instantiate(letterObject, transform.position, Quaternion.identity);
            newLetterObject.transform.SetParent(transform, false);
            letters[i] = newLetterObject.GetComponent<LetterController>();
        }
    }

    public char[] getUniqueLetters()
    {
        return baseWord.Distinct().ToArray();
    }

    public bool addLeter(char newLetter)
    {
        for (int i=0; i< letters.Length; i++)
        {
            if (letters[i].isCorrect() || !letters[i].getLetter().Equals(' ')) continue;
            letters[i].setLetter(newLetter);
            return true;
        }
        return false;
    }

    public bool removeLetter()
    {
        for (int i=letters.Length-1; i>=0; i--)
        {
            if (letters[i].isCorrect() || letters[i].getLetter().Equals(' ')) continue;
            letters[i].removeLetter();
            return true;
        }
        return false;
    }

    public int letterQuantity()
    {
        return letters.Length;
    }

    public bool isFill()
    {
        foreach (LetterController letter in letters)
        {
            if (letter.getLetter().Equals(' ')) return false;
        }
        return true;
    }

    public char[] checkWordCorrect()
    {
        if (letters.Length != baseWord.Length) throw new Exception("The letters and the base word does not match.");
        List<char> incorrectLetters = new List<char>();
        for (int i=0; i<letters.Length; i++)
        {
            if (!letters[i].getLetter().Equals(baseWord[i]))
            {
                incorrectLetters.Add(letters[i].getLetter());
                letters[i].setState(LetterController.letterStates.idle);
                letters[i].removeLetter();
                continue;
            }
            letters[i].setState(LetterController.letterStates.correct);
        }
        return incorrectLetters.ToArray();
    }
}
