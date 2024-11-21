using UnityEngine;
using UnityEngine.SceneManagement;

public class AA_GameManager : MonoBehaviour
{

    private bool game_over = false;
    
    // public Animator animator;

    // AA Game Components
    
    public AA_Rotator rotator;
    public AA_Spawner spawner;


    public void RestartLevel ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public void goBack(){
        // Go to 0th index scene
        SceneManager.LoadScene(0);
    }

    public void EndGame(){
        if (game_over) return;
        
        game_over = true;
        
        rotator.enabled = false;
        spawner.enabled = false;

        // animator.SetTrigger("EndGame");

        Debug.Log("Game Over");
        RestartLevel();
    }
}
