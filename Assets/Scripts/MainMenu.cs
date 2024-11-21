using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Triggered");
        Application.Quit();
    }

    public void muteAudio()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void PlayGame(String gameName)
    {
        SceneManager.LoadScene(gameName);
    }

    public void PlayColorShift()
    {
        PlayGame("ColorShift");
    }

    public void PlayColorSwitch()
    {
        PlayGame("ColorSwitch");
    }

    public void Play2048()
    {
        PlayGame("2048");
    }

    public void PlayAA()
    {
        PlayGame("AA");
    }

    
}
