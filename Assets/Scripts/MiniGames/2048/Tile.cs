using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public bool isLocked { get; set; }
    private Image tile_color;
    private TextMeshProUGUI tile_text;
    public int value { get; private set; }

    private void Awake(){
        tile_color = GetComponent<Image>();
        tile_text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetState(TileState state, int value){
        Debug.Log("Setting tile state");
        this.state = state;
        this.value = value;
        tile_color.color = state.bgColor;
        tile_text.text = value.ToString();
    }
    public void Spawn(TileCell cell){
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;
        
        transform.position = cell.transform.position;
    }
    public void MoveTo(TileCell cell){
        Debug.Log("Moving tile");
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;
        StartCoroutine(Animate(cell.transform.position, false));
        Debug.Log("MoveTo complete");
    }

    public void Merge(TileCell cell){
        Debug.Log("Merging");
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = null;
        cell.tile.isLocked = true; // Lock the tile to prevent it from double merging in the same move
        StartCoroutine(Animate(cell.transform.position, true));
        Debug.Log("Merge complete");
    }

    private IEnumerator Animate(Vector3 targetPos, bool merging){
        Debug.Log("Animating");
        float duration = 0.1f;
        float elapsed = 0;
        Vector3 startPos = transform.position;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        if (merging)
        {
            Destroy(gameObject);
        }
    }
}