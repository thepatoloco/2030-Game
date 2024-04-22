using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParagraphController : MonoBehaviour
{
    [SerializeField]
    private GameObject sentenceObject;
    [SerializeField]
    private MissingLettersController missingLettersController;

    private SentenceSection[][] paragraph = new SentenceSection[0][];
    private List<WordController> words = new List<WordController>();
    private List<char> missingLetters = new List<char>();


    public void setParagraph(SentenceSection[][] paragraph)
    {
        this.paragraph = paragraph;
        List<WordController> words = new List<WordController>();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (SentenceSection[] sentence in this.paragraph)
        {
            GameObject newSentence = Instantiate(sentenceObject);
            newSentence.transform.SetParent(transform, false);
            SentenceController newSentenceController = newSentence.GetComponent<SentenceController>();

            List<WordController> sentenceWords = newSentenceController.setSentence(sentence);
            foreach (WordController word in sentenceWords)
            {
                words.Add(word);
            }
        }

        this.words = words;
        missingLetters = new List<char>();
        missingLettersController.setMissingLetters(missingLetters);
    }

    public void addLetter(char letter)
    {
        foreach (WordController wordController in words)
        {
            if (wordController.addLeter(letter)) break;
        }
    }

    public void removeLetter()
    {
        List<WordController> words = new List<WordController>(this.words);
        words.Reverse();
        foreach (WordController wordController in words) {
            if (wordController.removeLetter()) break;
        }
    }

    public void addMissingLetters(List<char> newMissingLetters)
    {
        missingLetters = missingLetters.Union(newMissingLetters).Distinct().ToList();
        missingLetters.Sort();
        string logContent = "Missing Letters: ";
        foreach (char letter in missingLetters)
        {
            logContent += letter + ", ";
        }
        missingLettersController.setMissingLetters(missingLetters);
        Debug.Log(logContent);
    }

    public bool isFill()
    {
        foreach (WordController word in words)
        {
            if (!word.isFill()) return false;
        }
        return true;
    }

    public bool checkCorrect()
    {
        if (!isFill()) return false;
        char[] allIncorrectLetters = new char[0]; // all the incorrect letters in users response
        char[] lettersInResponse = new char[0]; // all the letters in the correct response
        foreach (WordController word in words)
        {
            char[] incorrectLetters = word.checkWordCorrect();
            allIncorrectLetters = allIncorrectLetters.Union(incorrectLetters).Distinct().ToArray();
            lettersInResponse = lettersInResponse.Concat(word.getUniqueLetters()).Distinct().ToArray();
        }
        List<char> missingLetters = new List<char>(); // every letter inputed by the user that is not in the answer
        foreach (char inputLetter in allIncorrectLetters)
        {
            if (!lettersInResponse.Contains(inputLetter)) missingLetters.Add(inputLetter);
        }
        addMissingLetters(missingLetters);
        return allIncorrectLetters.Length <= 0;
    }
}
