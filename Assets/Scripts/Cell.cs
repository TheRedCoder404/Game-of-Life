using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject cellPefab;
    [SerializeField] private Color lifeColor, deathColor;
    
    private Cell[] neighbors = new Cell[8];
    private int x, y;

    private bool _live, nextState, initialState;

    private bool live
    {
        get => _live;
        set
        {
            gameObject.GetComponent<SpriteRenderer>().color = value ? lifeColor : deathColor;
            _live = value;
        }
    }

    private void Start()
    {
        ApplyState();
    }

    public Cell CreateCell(int x, int y, float multiplier, GameObject parent)
    {
        Vector3 position = new Vector3(x * multiplier, y * multiplier, 0);
        Cell newCell = Instantiate(cellPefab, position, Quaternion.identity, parent.transform).GetComponent<Cell>();
        newCell.x = x;
        newCell.y = y;
        return newCell;
    }

    private void SetNeighbor(Cell neighbor, int direction)
    {
        neighbors[direction] = neighbor;
    }

    public void AssignNeighbors(Cell[,] grid, bool wrapAround, int height, int width)
    {
        for (int i = 0; i < NeighborDirectionExtensions.IndexValues.Length; i++)
        {

            if (!wrapAround)
            {
                int nx = x + NeighborDirectionExtensions.IndexValues[i].X;
                int ny = y + NeighborDirectionExtensions.IndexValues[i].Y;
                
                if (nx >= 0 && nx < width && ny >= 0 && y < height)
                {
                    try
                    {
                        neighbors[i] = grid[ny, nx];
                    }
                    catch (Exception)
                    {
                        // do nothing
                    }
                }
            }
            else
            {
                int nx = (x + NeighborDirectionExtensions.IndexValues[i].X + width) % width;
                int ny = (y + NeighborDirectionExtensions.IndexValues[i].Y + height) % height;

                neighbors[i] = grid[ny, nx];
            }
        }
    }

    public void UpdateState()
    {
        int neighborsAlive = GetLiveNeighbors();
        
        nextState = live ? neighborsAlive is 2 or 3 : neighborsAlive is 3;
    }

    public void ApplyState()
    {
        live = nextState;
    }

    private int GetLiveNeighbors()
    {
        int alive = 0;
        foreach (Cell neighbor in neighbors)
        {
            if (!neighbor) continue;
            if (neighbor.live) alive++;
        }
        
        return alive;
    }

    public void RandomizeLive(float randomness)
    {
        nextState = Random.Range(0f, 1f) <= randomness;
        initialState = nextState;
    }

    private void OnMouseDown()
    {
        live = !live;
    }

    public void Reset()
    {
        live = initialState;
    }
}
