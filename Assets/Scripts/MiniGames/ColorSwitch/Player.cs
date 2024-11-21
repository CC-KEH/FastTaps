using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jump_force = 3f;
    public string current_color;

    public Rigidbody2D rigid_body;
    public SpriteRenderer sprite_renderer;

    public Color cyan_color;
    public Color yellow_color;
    public Color pink_color;
    public Color magenta_color;

    public GameObject single_circle;
    public GameObject double_circle;
    public GameObject triple_circle;
    public GameObject color_changer;
    public TMP_Text scoreText;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ColorChanger")
        {
            SetRandomColor();
            Destroy(col.gameObject);
            return;
        }

        if (col.tag == "wall")
        {
            // Continue Generating world
            Debug.Log("Wall hit");
            Vector3 last_position = col.transform.position;
            float y_position = last_position.y + 5f;
            float x_position = last_position.x;
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    Instantiate(single_circle, new Vector3(x_position, y_position, 0), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(double_circle, new Vector3(x_position, y_position, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(triple_circle, new Vector3(x_position, y_position, 0), Quaternion.identity);
                    break;
            }
            Instantiate(color_changer, new Vector3(x_position, y_position + 7.5f, 0), Quaternion.identity);
            Destroy(col.gameObject);
            // Generate new wall at 8f from last wall
            Instantiate(col.gameObject, new Vector3(x_position, y_position + 8f, 0), Quaternion.identity);
        }

        else
        {
            if (col.tag != current_color)
            {
                Debug.Log("GAME OVER!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void SetRandomColor()
    {
        int index = Random.Range(0, 4);
        switch (index)
        {
            case 0:
                current_color = "Cyan";
                sprite_renderer.color = cyan_color;
                break;
            case 1:
                current_color = "Yellow";
                sprite_renderer.color = yellow_color;
                break;
            case 2:
                current_color = "Pink";
                sprite_renderer.color = pink_color;
                break;
            case 3:
                current_color = "Magenta";
                sprite_renderer.color = magenta_color;
                break;
        }
        Debug.Log("Color changed to: " + current_color + ", SpriteRenderer color: " + sprite_renderer.color);
    }
    void updateScore()
    {
        int score = (int)transform.position.y + 3; // +3 to make the score start from 0
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = score.ToString();
    }

    void Start()
    {
        SetRandomColor();
    }

    // void Update()
    // {
    //     if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //     {
    //         rigid_body.linearVelocity = Vector2.up * jump_force;
    //     }
    //     if (transform.position.y < -6.0f)
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //     }
    //     updateScore();
    // }

    void Update()
    {
        var mouse = Mouse.current;
        var keyboard = Keyboard.current;
        var touch = Touchscreen.current;

        if ((keyboard != null && keyboard.spaceKey.wasPressedThisFrame) ||
            (mouse != null && mouse.leftButton.wasPressedThisFrame) ||
            (touch != null && touch.primaryTouch.press.wasPressedThisFrame && touch.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began))
        {
            rigid_body.linearVelocity = Vector2.up * jump_force;
        }

        if (transform.position.y < -6.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        updateScore();
    }
}
