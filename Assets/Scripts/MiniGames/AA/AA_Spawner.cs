using UnityEngine;
using UnityEngine.InputSystem;

public class AA_Spawner : MonoBehaviour
{
    public GameObject pinPrefab;

    void spawnPin()
    {
        Instantiate(pinPrefab, transform.position, transform.rotation);
    }

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //     {
    //         spawnPin();
    //     }
    // }
    // 
    void Update()
    {
        var mouse = Mouse.current;
        var keyboard = Keyboard.current;
        var touch = Touchscreen.current;

        if ((mouse != null && mouse.leftButton.wasPressedThisFrame) ||
            (keyboard != null && keyboard.spaceKey.wasPressedThisFrame) ||
            (touch != null && touch.primaryTouch.press.wasPressedThisFrame && touch.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began))
        {
            spawnPin();
        }
    }
}
