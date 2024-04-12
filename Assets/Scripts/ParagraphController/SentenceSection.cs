using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SentenceSection
{
    [SerializeField]
    private string content;
    [SerializeField]
    private bool guess;

    public SentenceSection(string content, bool guess)
    {
        this.content = content;
        this.guess = guess;
    }

    public string getContent()
    {
        return content;
    }

    public bool isGuess()
    {
        return guess;
    }
}
