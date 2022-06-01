using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowInstruction()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
