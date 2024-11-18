using UnityEngine;

public class AA_GameManager : MonoBehaviour
{

    private bool game_over = false;
    
    // public Animator animator;

    // AA Game Components
    public AA_Rotator rotator;
    public AA_Spawner spawner;



    public void EndGame(){
        if (game_over) return;
        
        game_over = true;
        
        rotator.enabled = false;
        spawner.enabled = false;

        // animator.SetTrigger("EndGame");

        Debug.Log("Game Over");
    } 
    
}
