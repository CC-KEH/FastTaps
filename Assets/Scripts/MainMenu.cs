using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoForward()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Triggered");
        Application.Quit();
    }

    public void PlayColorShift(){
        // SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(2).name);
    }
    public void PlayColorSwitch(){
        // SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(3).name);
    }
    public void PlaySudoku(){
        // SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(4).name);
    }
    public void PlayAA(){
        // SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(5).name);
    }
}
