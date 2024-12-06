using System.Collections.Generic;
using UnityEngine;

public class SpatialPartition : MonoBehaviour
{
    private Dictionary<Vector2Int, List<int>> grid;
    private float cellSize;

    public SpatialPartition(float cellSize)
    {
        this.cellSize = cellSize;
        grid = new Dictionary<Vector2Int, List<int>>();
    }

    public void Clear()
    {
        grid.Clear();
    }

    public void AddParticle(Vector3 position, int particleIndex)
    {
        Vector2Int cell = GetCell(position);
        if (!grid.ContainsKey(cell))
        {
            grid[cell] = new List<int>();
        }
        grid[cell].Add(particleIndex);
    }

    public List<int> GetParticlesInCell(Vector3 position)
    {
        Vector2Int cell = GetCell(position);
        return grid.ContainsKey(cell) ? grid[cell] : new List<int>();
    }

    private Vector2Int GetCell(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / cellSize),
            Mathf.FloorToInt(position.z / cellSize) // Assuming a 2D plane; adapt for 3D if necessary
        );
    }
}
