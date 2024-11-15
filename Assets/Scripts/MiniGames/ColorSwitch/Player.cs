using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jump_force = 10f;
    public string current_color;

    public Rigidbody2D rigid_body;
    public SpriteRenderer sprite_renderer;

    public Color cyan_color;
    public Color yellow_color;
    public Color pink_color;
    public Color magenta_color;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ColorChanger")
        {
            SetRandomColor();
            Destroy(col.gameObject);
            return;
        }

        if (col.tag!=current_color)
        {
            Debug.Log("GAME OVER!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    void Start()
    {
        SetRandomColor();
    }

    void Update()
    {   
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rigid_body.linearVelocity = Vector2.up * jump_force;
        }
    }
}
