using TMPro;
using UnityEngine;

public class AA_Score : MonoBehaviour
{
    public static int pin_count = 0;
    public TMP_Text text;

    void Start()
    {
        pin_count = 0;
    }

    void Update()
    {
        text.text = pin_count.ToString();
    }
}