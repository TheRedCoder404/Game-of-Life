using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private float distanceMultiplier = 1.0f;

    private bool gridDone, simRunning, updating;
    private Cell[,] grid;

    private float timer; 
    public float timeToWait;
    
    private void Start()
    {
        gridDone = false;
        simRunning = false;
        updating = false;
        timer = 0;
    }

    public void CreateGrid(int width, int height, bool randomPattern, float randomness)
    {
        gridDone  = false;
        if (grid != null) DeleteGrid();
        
        grid = new Cell[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[y, x] = cellPrefab.CreateCell(x, y,distanceMultiplier, gameObject);
            }
        }
        
        if (randomPattern) RandomizeGrid(randomness);
        AssignNeighbors();
        gridDone = true;
    }
    
    private void DeleteGrid()
    {
        foreach (Cell cell in grid)
        {
            Destroy(cell.gameObject);
        }
    }

    public void RandomizeGrid(float randomness)
    {
        updating = true;
        foreach (Cell cell in grid)
        {
            cell.RandomizeLive(randomness);
        }
        updating = false;
    }

    private void AssignNeighbors()
    {
        foreach (Cell cell in grid)
        {
            cell.AssignNeighbors(grid);
        }
    }

    private void Update()
    {
        if (gridDone && simRunning && !updating)
        {
            if (timer >= timeToWait)
            {
                timer = 0;
                UpdateCells();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    public void StartSim()
    {
        simRunning = true;
    }

    public void StopSim()
    {
        simRunning = false;
    }

    private void UpdateCells()
    {
        updating = true;
        foreach (Cell cell in grid) cell.UpdateState();
        foreach (Cell cell in grid) cell.ApplyState();
        updating = false;
    }
}
