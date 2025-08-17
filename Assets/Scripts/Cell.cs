using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject cellPefab;
    
    private Cell[] neighbors = new Cell[8];
    private int x, y;

    private bool _live, nextState;

    private bool live
    {
        get => _live;
        set
        {
            gameObject.GetComponent<SpriteRenderer>().color = value ? lifeColor : deathColor;
            _live = value;
        }
    }

    private Color lifeColor, deathColor;

    private void Start()
    {
        nextState = false;
        ApplyState();
        lifeColor = Color.white;
        deathColor = Color.gray3;
        gameObject.GetComponent<SpriteRenderer>().color = deathColor;
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

    public void AssignNeighbors(Cell[,] grid)
    {
        for (int i = 0; i < NeighborDirectionExtensions.IndexValues.Length; i++)
        {
            try
            {
                SetNeighbor(grid[y + NeighborDirectionExtensions.IndexValues[i].Y, x + NeighborDirectionExtensions.IndexValues[i].X], i);
            }
            catch (IndexOutOfRangeException)
            {
                // do nothing
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
        float random = Random.Range(0f, 1f);
        bool newValue = random <= randomness;
        live = newValue;
    }

    private void OnMouseDown()
    {
        live = !live;
    }
}
