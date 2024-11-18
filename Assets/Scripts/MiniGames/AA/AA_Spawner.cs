using UnityEngine;

public class AA_Spawner : MonoBehaviour
{
    public GameObject pinPrefab;

    void spawnPin()
    {
        Instantiate(pinPrefab, transform.position, transform.rotation);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            spawnPin();
        }
    }
}
