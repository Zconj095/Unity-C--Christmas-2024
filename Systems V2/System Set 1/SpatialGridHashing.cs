using System.Collections.Generic;
using UnityEngine;

public class SpatialGridHashing
{
    private Dictionary<int, List<int>> grid; // Map grid cell ID to particle indices
    private float cellSize;

    public SpatialGridHashing(float cellSize)
    {
        this.cellSize = cellSize;
        grid = new Dictionary<int, List<int>>();
    }

    // Clear the grid for a new update
    public void Clear()
    {
        grid.Clear();
    }

    // Add a particle to the grid
    public void Add(Vector3 position, int particleIndex)
    {
        int cellID = GetCellID(position);

        if (!grid.ContainsKey(cellID))
        {
            grid[cellID] = new List<int>();
        }
        grid[cellID].Add(particleIndex);
    }

    // Get particles in the cell containing the specified position
    public List<int> GetNeighbors(Vector3 position)
    {
        int cellID = GetCellID(position);
        return grid.ContainsKey(cellID) ? grid[cellID] : new List<int>();
    }

    // Hash function to calculate a unique cell ID for 3D coordinates
    private int GetCellID(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / cellSize);
        int y = Mathf.FloorToInt(position.y / cellSize);
        int z = Mathf.FloorToInt(position.z / cellSize);

        // Combine coordinates into a unique hash
        int hash = x + y * 73856093 + z * 19349663; // Use large primes for minimal collisions
        return hash;
    }
}
