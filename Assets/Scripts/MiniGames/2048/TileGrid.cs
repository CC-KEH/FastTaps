using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >=0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        }else{
            return null;
        }
    }   

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int newCoordinates = cell.coordinates;
        newCoordinates.x += direction.x;
        newCoordinates.y -= direction.y;
        return GetCell(newCoordinates.x, newCoordinates.y);
    }

    private void Start()
    {
        // Assign coordinates to each cell
        // rid = row idx, cid = cell idx
        for (int rid = 0; rid < rows.Length; rid++)
        {
            for (int cid = 0; cid < rows[rid].cells.Length; cid++)
            {
                rows[rid].cells[cid].coordinates = new Vector2Int(cid, rid);
            }
        }
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, size);
        int st_index = index;

        while (cells[index].occupied)
        {   
            index++;
            // If we have reached the end of the array, start from the beginning
            if (index >= size)
            {
                index = 0;
            }
            // If we have checked all cells and none are empty
            if (index == st_index) 
            {
                return null;
            }
        }
        return cells[index];
    }
}
