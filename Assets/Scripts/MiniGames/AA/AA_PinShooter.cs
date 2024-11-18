using UnityEngine;

public class AA_PinShooter : MonoBehaviour
{
    public float speed = 20f;   
    public Rigidbody2D rigid_body;
    private bool isPinned = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Rotator") {
            transform.SetParent(other.transform);
            AA_Rotator rotator = other.GetComponent<AA_Rotator>();
            if (rotator != null) {
                rotator.speed *= -1;
                AA_Score.pin_count++;
                isPinned = true;
            } else {
                Debug.LogError($"AA_Rotator component not found on the object with tag 'Rotator'. Object name: {other.name}");
            }
        }
        else if (other.tag == "Pin") {
            AA_GameManager gameManager = FindFirstObjectByType<AA_GameManager>();
            if (gameManager != null) {
                gameManager.EndGame();
            } else {
                Debug.LogError("AA_GameManager not found");
            }
        }
    }

    void Update()
    {
        if (!isPinned)
        {
            rigid_body.MovePosition(rigid_body.position + Vector2.up * speed * Time.deltaTime);
        }
    }
}