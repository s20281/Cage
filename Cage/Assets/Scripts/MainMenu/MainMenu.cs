using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SaveSystem saveSystem;

    public void StartGame()
    {
        PlayerPrefs.SetInt("loadGame", 0);
        saveSystem.newGame();
        SceneManager.LoadScene("Level 1");
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("loadGame", 1);
        StaticClass.loadScene = true;
        SceneManager.LoadScene(PlayerPrefs.GetString("level"));
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
