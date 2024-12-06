using System.Collections.Generic;
using UnityEngine;

public class NanovoxelGrid : MonoBehaviour
{
    [Tooltip("Size of the grid in voxels (width, height, depth).")]
    public Vector3Int gridSize = new Vector3Int(10, 10, 10);

    [Tooltip("Size of each voxel in world units.")]
    public float voxelSize = 0.1f;

    [Tooltip("Default color for active voxels.")]
    public Color defaultColor = Color.gray;

    private Nanovoxel[,,] voxels;

    void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        voxels = new Nanovoxel[gridSize.x, gridSize.y, gridSize.z];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3 position = transform.position + new Vector3(x, y, z) * voxelSize;
                    voxels[x, y, z] = new Nanovoxel(position, defaultColor, 1.0f, false);
                }
            }
        }

        Debug.Log($"Initialized nanovoxel grid with {gridSize.x * gridSize.y * gridSize.z} voxels.");
    }

    public void ActivateVoxel(Vector3Int gridPosition, Color color, float density)
    {
        if (IsValidGridPosition(gridPosition))
        {
            Nanovoxel voxel = voxels[gridPosition.x, gridPosition.y, gridPosition.z];
            voxel.isActive = true;
            voxel.color = color;
            voxel.density = density;
        }
    }

    private bool IsValidGridPosition(Vector3Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < gridSize.x &&
               gridPosition.y >= 0 && gridPosition.y < gridSize.y &&
               gridPosition.z >= 0 && gridPosition.z < gridSize.z;
    }

    void OnDrawGizmos()
    {
        if (voxels == null) return;

        Gizmos.color = Color.white;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Nanovoxel voxel = voxels[x, y, z];
                    if (voxel.isActive)
                    {
                        Gizmos.color = voxel.color;
                        Gizmos.DrawCube(voxel.position, Vector3.one * voxelSize);
                    }
                }
            }
        }
    }
}
