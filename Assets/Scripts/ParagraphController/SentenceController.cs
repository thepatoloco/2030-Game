using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SentenceController : MonoBehaviour
{
    [SerializeField]
    private GameObject sectionObject;
    [SerializeField]
    private GameObject wordObject;

    private SentenceSection[] sentenceSections = new SentenceSection[0];


    public List<WordController> setSentence(SentenceSection[] sentenceSections)
    {
        this.sentenceSections = sentenceSections;
        List<WordController> wordControllers = new List<WordController>();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (SentenceSection sentenceSection in sentenceSections)
        {
            if (!sentenceSection.isGuess())
            {
                GameObject newSection = Instantiate(sectionObject);
                newSection.transform.SetParent(transform, false);
                TextMeshProUGUI newSectionText = newSection.GetComponent<TextMeshProUGUI>();
                newSectionText.text = sentenceSection.getContent();
                continue;
            }
            GameObject newWord = Instantiate(wordObject);
            newWord.transform.SetParent(transform, false);
            WordController newWordController = newWord.GetComponent<WordController>();
            newWordController.setBaseWord(sentenceSection.getContent());

            wordControllers.Add(newWordController);
        }

        return wordControllers;
    }
}
