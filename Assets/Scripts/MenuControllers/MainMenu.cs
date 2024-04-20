using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject quitButton;
    public GameObject difficultyButton1;
    public GameObject difficultyButton2;
    public GameObject difficultyButton3;
    public GameObject backButton;
    public GameObject creditsName1;
    public GameObject creditsName2;
    public GameObject creditsName3;

    private string selectedDifficulty;

    public void PlayGame()
    {
        // Mostrar botones
        difficultyButton1.SetActive(true);
        difficultyButton2.SetActive(true);
        difficultyButton3.SetActive(true);
        backButton.SetActive(true);

        // Ocultar botones
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void BackButton()
    {
        // Ocultar botones
        difficultyButton1.SetActive(false);
        difficultyButton2.SetActive(false);
        difficultyButton3.SetActive(false);
        backButton.SetActive(false);
        creditsName1.SetActive(false);
        creditsName2.SetActive(false);
        creditsName3.SetActive(false);

        // Mostrar botones
        playButton.SetActive(true);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void GoToCreditsMenu()
    {
        // Ocultar botones
        difficultyButton1.SetActive(false);
        difficultyButton2.SetActive(false);
        difficultyButton3.SetActive(false);
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);

        // Mostrar botones
        backButton.SetActive(true);
        creditsName1.SetActive(true);
        creditsName2.SetActive(true);
        creditsName3.SetActive(true);
    }

    public void EasyDifficulty()
    {
        selectedDifficulty = "Easy";
        PlayerPrefs.SetString("selectedDifficulty", selectedDifficulty);
        SceneManager.LoadScene(2);
    }

    public void MediumDifficulty()
    {
        selectedDifficulty = "Medium";
        PlayerPrefs.SetString("selectedDifficulty", selectedDifficulty);
        SceneManager.LoadScene(2);

    }

    public void HardDifficulty()
    {
        selectedDifficulty = "Hard";
        PlayerPrefs.SetString("selectedDifficulty", selectedDifficulty);
        SceneManager.LoadScene(2);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
