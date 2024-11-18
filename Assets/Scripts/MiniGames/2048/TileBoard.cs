using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public TileGameManager tileGameManager;
    public Tile tilePrefab;
    private TileGrid grid;
    private List<Tile> tiles;
    public TileState[] tileStates;
    private bool waitingForMove;
    public void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);
    }

    public void ClearBoard()
    {
        foreach(TileCell cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach (Tile tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        
        tiles.Clear();
    }

    public void CreateTile()
    {
        // Create a new tile at a random position on the grid
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {   
        Debug.Log("MoveTiles called with direction: " + direction);
        bool changed = false;
        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);
                if (cell.occupied)
                {
                    changed |= MoveTile(cell.tile, direction);
                }
            }
        }
        if (changed)
        {
            StartCoroutine(WaitForChanges());
        }
    }
    private bool CanMerge(Tile a, Tile b)
    {
        Debug.Log("Checking Merge");
        return a.value == b.value && !b.isLocked;
    }
    private void Merge(Tile a, Tile b)
    {
        Debug.Log("Merging Tiles");
        tiles.Remove(a);
        a.Merge(b.cell);
        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length - 1);
        int value = b.value * 2;
        b.SetState(tileStates[index], value);
        tileGameManager.IncreaseScore(value);
    }
    
    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        Debug.Log("Moving Tile");
        TileCell newCell = null;
        TileCell adjacentCell = grid.GetAdjacentCell(tile.cell, direction);
        while (adjacentCell != null)
        {
            if (adjacentCell.occupied)
            {
                // Merge tiles
                if (CanMerge(tile, adjacentCell.tile))
                {
                    Merge(tile, adjacentCell.tile);
                    return true;
                }
                break;
            }
            newCell = adjacentCell;
            adjacentCell = grid.GetAdjacentCell(adjacentCell, direction);
        }
        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }
        return false;
    }
    
    private int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (tileStates[i] == state)
            {
                return i;
            }
        }
        return -1;
    }

    private IEnumerator WaitForChanges()
    {   
        Debug.Log("Waiting for Changes");
        waitingForMove = true;
        yield return new WaitForSeconds(0.1f);
        waitingForMove = false;
        foreach (Tile tile in tiles)
        {
            tile.isLocked = false;
        }
        if (tiles.Count != grid.width * grid.height)
        {
            CreateTile();
        }
        if (CheckForGameOver())
        {
            tileGameManager.GameOver();
        }
    }

    private bool CheckForGameOver()
    {
        Debug.Log("Checking for Game Over");
        if (tiles.Count!= grid.width * grid.height)
        {
            return false;
        }
        // Check if there are any possible merges
        foreach (Tile tile in tiles)
        {
            TileCell upperCell = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell lowerCell = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell leftCell = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            TileCell rightCell = grid.GetAdjacentCell(tile.cell, Vector2Int.right);
            if (upperCell != null && CanMerge(tile, upperCell.tile))
            {
                return false;
            }
            if (lowerCell != null && CanMerge(tile, lowerCell.tile))
            {
                return false;
            }
            if (leftCell != null && CanMerge(tile, leftCell.tile))
            {
                return false;
            }
            if (rightCell != null && CanMerge(tile, rightCell.tile))
            {
                return false;
            }

        }
        return true;
    }

    private void Update()
    {
        if (waitingForMove) return;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Up");
                Move(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Down");
                Move(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Left");
                Move(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Right");
                Move(Vector2Int.right, grid.width - 2, -1, 0, 1);
            }
    }

}
