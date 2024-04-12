using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private ParagraphController paragraph;

    // Start is called before the first frame update
    void Start()
    {
        paragraph = GetComponent<ParagraphController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && Input.inputString.Length > 0 && char.IsLetter(Input.inputString[0]))
        {
            Debug.Log("Tecla presionada: " + Input.inputString);
            paragraph.addLetter(char.ToLower(Input.inputString[0]));
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Eliminar letra.");
            paragraph.removeLetter();
        }
    }
}
