using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && Input.inputString.Length > 0 && char.IsLetter(Input.inputString[0]))
        {
            Debug.Log("Tecla presionada: " + Input.inputString);
        }
    }
}
