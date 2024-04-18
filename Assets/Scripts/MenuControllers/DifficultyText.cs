using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyText : MonoBehaviour
{

    public TextMeshProUGUI difficultyText;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("selectedDifficulty"))
        {
            string selectedDifficulty = PlayerPrefs.GetString("selectedDifficulty");
            difficultyText.text = "Selected difficulty: " + selectedDifficulty;
        } else
        {
            difficultyText.text = "No difficulty selected";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
